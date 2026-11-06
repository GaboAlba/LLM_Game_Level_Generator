namespace PcgBenchmark.Helpers
{
    using LLMPromptProcessor;
    using LLMPromptProcessor.PromptTemplates;

    using System.ClientModel;
    using System.Collections.Generic;
    using System.Text.Json;

    internal static class ConsoleHelper
    {
        private const int MaxTokensPerDay = 950000;
        private const int MaxConcurrentRequests = 16;

        // Tokens assumed for one model attempt (prompt + up to the 20k max output). Reserved while a
        // call is in flight, so concurrent calls can't overshoot the daily cap, and charged for each
        // failed attempt the API may still bill (e.g. a client timeout: the generation still runs).
        private const int TokensPerAttemptEstimate = 25000;

        /// <summary>
        /// Tokens used so far in the current UTC day, persisted next to the results file so a
        /// restart keeps counting against the same day instead of starting again from zero.
        /// </summary>
        private sealed class TokenLedger
        {
            public string DateUtc { get; set; } = CurrentUtcDate();

            public long Tokens { get; set; }

            public static TokenLedger Load(string path)
            {
                var ledger = File.Exists(path)
                    ? JsonSerializer.Deserialize<TokenLedger>(File.ReadAllText(path)) ?? new TokenLedger()
                    : new TokenLedger();
                ledger.RollOver();
                return ledger;
            }

            /// <summary>Resets the count once the UTC day has changed.</summary>
            public void RollOver()
            {
                if (this.DateUtc != CurrentUtcDate())
                {
                    this.DateUtc = CurrentUtcDate();
                    this.Tokens = 0;
                }
            }

            public void Add(long tokens, string path)
            {
                this.RollOver();
                this.Tokens += tokens;
                File.WriteAllText(path + ".tmp", JsonSerializer.Serialize(this));
                File.Move(path + ".tmp", path, overwrite: true);
            }

            private static string CurrentUtcDate() => DateTime.UtcNow.ToString("yyyy-MM-dd");
        }

        internal struct ConsoleOutput
        {
            public ConsoleOutput()
            {
                this.BenchmarksToRun = new Dictionary<string, object>();
                this.RawOutput = new Dictionary<string, string>();
                this.Output = new Dictionary<string, List<List<string>>>();
                this.DebugMessage = string.Empty;
                this.Error = [];
            }

            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, object> BenchmarksToRun { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, string> RawOutput { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, List<List<string>>> Output { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public string DebugMessage { get; internal set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> Error { get; internal set; }
        }

        internal async static Task<ConsoleOutput?> HandleRequestAsync(string[] args)
        {
            var output = new ConsoleOutput();
            var model = "gpt-5.2";
            var numberOfIterations = 1;
            if (args.Length == 0)
            {
                output.BenchmarksToRun = BenchmarkHelper.GetAllPossibleBenchmarks();
                numberOfIterations = 1;
            }
            else
            {
                for (var i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--help":
                            output.DebugMessage = GetHelpMessage();
                            return output;
                        case "-b":
                            output.BenchmarksToRun = BenchmarkHelper.GetBenchmarksToRun(args[i + 1]);
                            break;
                        case "-m":
                            var userModel = args[i + 1];
                            if (LlmHelper.IsValidModel(userModel, out var validModels))
                            {
                                model = userModel;
                                break;
                            }
                            else
                            {
                                return new ConsoleOutput() { Error = [$"Model must be one of \n\n {validModels}"] };
                            }
                        case "-n":
                            _ = int.TryParse(args[i + 1], out numberOfIterations);
                            break;

                    }
                }
            }

            // Add all iterations to the benchmarks
            var tempDict = new Dictionary<string, object>();
            foreach (var kvp in output.BenchmarksToRun)
            {
                for (var i = 0; i < numberOfIterations; i++)
                {
                    tempDict.Add(kvp.Key + $"_{i}", kvp.Value);
                }
            }

            output.BenchmarksToRun = tempDict;
            var toRun = new Dictionary<string, object>(output.BenchmarksToRun);

            // Call the model
            var handleBarsEngine = new HandlebarsEngine();
            var jsonPath = "..\\..\\..\\..\\tools\\pcg_benchmark\\pcg_results.json";
            var tempFile = string.Concat(jsonPath, ".tmp");
            if (File.Exists(jsonPath))
            {
                File.Copy(jsonPath, jsonPath + ".old", true);
                var fileContent = File.ReadAllText(jsonPath);
                var saved = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(fileContent);
                output.RawOutput = saved["RawOutput"].Deserialize<Dictionary<string, string>>() ?? [];
                output.Output = saved["Output"].Deserialize<Dictionary<string, List<List<string>>>>() ?? [];

                // Remove benchmarks already ran
                toRun = output.BenchmarksToRun
                          .Where(kvp => !output.Output.ContainsKey(kvp.Key))
                          .ToDictionary();
                if (toRun.Count == 0)        // everything already done → fresh run
                {
                    output.RawOutput.Clear();
                    output.Output.Clear();
                    toRun = new Dictionary<string, object>(output.BenchmarksToRun);
                }
            }

            var lockObj = new object();
            var tokenLedgerPath = jsonPath + ".tokens";
            var ledger = TokenLedger.Load(tokenLedgerPath);
            Console.WriteLine($"Tokens already used today ({ledger.DateUtc} UTC): {ledger.Tokens}");
            long reservedTokens = 0;
            var currentConcurrentRequests = 0;
            var quotaExhausted = false;
            var waitAnnouncedFor = string.Empty;
            var tasks = toRun.Select(async benchmark =>
            {
                var prompt = handleBarsEngine.ParsePrompt(benchmark.Value as PromptTemplateV2);
                if (prompt == null)
                {
                    return;
                }

                // Wait for a free slot within today's token budget, then reserve it.
                while (true)
                {
                    TimeSpan? wait = null;
                    lock (lockObj)
                    {
                        if (quotaExhausted)
                        {
                            return;
                        }

                        ledger.RollOver();
                        if (ledger.Tokens + reservedTokens + TokensPerAttemptEstimate > MaxTokensPerDay)
                        {
                            if (reservedTokens > 0)
                            {
                                // In-flight calls may finish under their reservation; check again soon.
                                wait = TimeSpan.FromMinutes(1);
                            }
                            else
                            {
                                // Budget spent for today. Sleep in short steps so a PC sleep can't
                                // stretch the wait past midnight UTC.
                                var untilReset = DateTime.UtcNow.Date.AddDays(1).AddMinutes(1) - DateTime.UtcNow;
                                wait = untilReset < TimeSpan.FromMinutes(10) ? untilReset : TimeSpan.FromMinutes(10);
                                if (waitAnnouncedFor != ledger.DateUtc)
                                {
                                    waitAnnouncedFor = ledger.DateUtc;
                                    Console.WriteLine($"Daily token budget used ({ledger.Tokens}/{MaxTokensPerDay}); waiting {untilReset} for the UTC day to reset");
                                }
                            }
                        }
                        else if (currentConcurrentRequests >= MaxConcurrentRequests)
                        {
                            wait = TimeSpan.FromMinutes(1);
                        }
                        else
                        {
                            currentConcurrentRequests++;
                            reservedTokens += TokensPerAttemptEstimate;
                        }
                    }

                    if (wait == null)
                    {
                        break;
                    }

                    await Task.Delay(wait.Value);
                }

                Console.WriteLine($"LLM Call for benchmark {benchmark.Key} has started");
                string? outputString = null;
                long tokensUsed;
                try
                {
                    (outputString, var tokenCount) = await LlmHelper.InvokeModelAsync(prompt, benchmark.Value as PromptTemplateV2, model);
                    tokensUsed = tokenCount;
                    Console.WriteLine($"LLM Call for benchmark {benchmark.Key} has successfully ended");
                }
                catch (Exception ex)
                {
                    tokensUsed = (long)TokensPerAttemptEstimate * CountBillableAttempts(ex);
                    lock (lockObj)
                    {
                        output.Error.Add(ex.ToString());
                        Console.WriteLine("CONSOLE_EXCEPTION: " + ex.ToString());
                        if (!quotaExhausted && IsQuotaExhausted(ex))
                        {
                            quotaExhausted = true;
                            Console.WriteLine("API quota exhausted: not starting any more calls in this run.");
                        }
                    }
                }

                lock (lockObj)
                {
                    // Charge the tokens and free the slot first, so a failed save below can't skip them.
                    currentConcurrentRequests--;
                    reservedTokens -= TokensPerAttemptEstimate;
                    try
                    {
                        ledger.Add(tokensUsed, tokenLedgerPath);
                        if (outputString != null)
                        {
                            output.RawOutput[benchmark.Key] = outputString;
                            output.Output[benchmark.Key] = BenchmarkHelper.ConvertToListOfLists(outputString);
                            var jsonString = JsonSerializer.Serialize(output);
                            File.WriteAllText(tempFile, jsonString);
                            File.Move(tempFile, jsonPath, overwrite: true);
                        }
                    }
                    catch (Exception ex)
                    {
                        output.Error.Add(ex.ToString());
                        Console.WriteLine("CONSOLE_EXCEPTION: " + ex.ToString());
                    }
                }
            });
            await Task.WhenAll(tasks);

            // Generate the JSON output file to be able to pass it to the Python benchmark runner
            var jsonString = JsonSerializer.Serialize(output);
            File.WriteAllText(jsonPath, jsonString);
            return output;
        }

        /// <summary>
        /// How many attempts of a failed call the API may have billed. The OpenAI client retries
        /// internally and reports every try in an <see cref="AggregateException"/>. Tries rejected
        /// up front (4xx, e.g. 429 rate limit / insufficient quota, 400 bad request) generate
        /// nothing; anything else (timeouts, dropped connections, bad output) is assumed billed.
        /// </summary>
        private static int CountBillableAttempts(Exception ex)
        {
            IEnumerable<Exception> attempts = ex is AggregateException aggregate ? aggregate.InnerExceptions : [ex];
            return attempts.Count(attempt => attempt is not ClientResultException { Status: >= 400 and < 500 });
        }

        private static bool IsQuotaExhausted(Exception ex)
        {
            IEnumerable<Exception> attempts = ex is AggregateException aggregate ? aggregate.InnerExceptions : [ex];
            return attempts.Any(attempt => attempt is ClientResultException { Status: 429 } clientError
                                           && clientError.Message.Contains("insufficient_quota"));
        }

        private static string GetHelpMessage()
        {
            return "Usage: PcgBenchmark -b [benchmark(s)] -m [Model]" +
                "The following properties might be passed\n\n" +
                "-b: (Optional) The specific benchmarks you want to run. The possibilities are\n" +
                "   * All: runs the benchmark on all the games\n" +
                "   * Binary: runs the benchmark on labyrinth type levels with only 2 types of tiles\n" +
                "   * DDave: runs the benchmark on Dangerous Dave game\n" +
                "   * Loderunner: run the benchmark on the Lode Runner game\n" +
                "   * Mdungeons: run the benchmark on the Mini Dungeons game\n" +
                "   * Sokoban: run the benchmark on the Sokoban game\n" +
                "   * Mario: runs the benchmark on Super Mario Bros\n" +
                "   * Zelda: runs the benchmark on the Zelda game\n" +
                "   * Debug: runs only the binary-v0 benchmark for quick testing\n" +
                "-m: (Optional) Specifies the model that will be run to generate the levels. Current allowed models are:\n" +
                "   * gpt-4.1: Non-reasoning text model with 1M token limit for the context window\n" +
                "   * gpt-5.2: Reasoning model with a 400k token limit for the context window";
        }
    }
}
