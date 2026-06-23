namespace PcgBenchmark.Helpers
{
    using LLMPromptProcessor;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;

    internal static class ConsoleHelper
    {
        private const int MaxTokensPerDay = 1000000;
        private const int MaxConcurrentRequests = 8;

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
            var model = string.Empty;
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

            // Call the model
            var handleBarsEngine = new HandlebarsEngine();
            var lockObj = new object();
            var currentTokenCountInDay = 0;
            var currentConcurrentRequests = 0;
            var tasks = output.BenchmarksToRun.Select(async benchmark =>
            {
                var prompt = handleBarsEngine.ParsePrompt(benchmark.Value as PromptTemplateV1);
                if (prompt != null)
                {
                    try
                    {
                        // Make sure we always check after 
                        while (true)
                        {
                            if (currentTokenCountInDay >= MaxTokensPerDay)
                            {
                                var midnightUTC = new DateTimeOffset(DateTime.UtcNow.Date.AddDays(1).AddMinutes(1)).ToUnixTimeMilliseconds();
                                var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

                                var timeUntilMidnightUTC = midnightUTC - now;
                                Console.WriteLine($"Milliseconds To Wait for {benchmark.Key}: {timeUntilMidnightUTC}");
                                await Task.Delay((int)timeUntilMidnightUTC);
                                currentTokenCountInDay = 0; // Reset count after waiting all day
                                Console.WriteLine($"Resuming processing for {benchmark.Key}");
                                continue;
                            }

                            if (currentConcurrentRequests >= MaxConcurrentRequests)
                            {
                                // Poll every minute until a thread is free
                                await Task.Delay(60000);
                                continue;
                            }

                            lock (lockObj)
                            {
                                currentConcurrentRequests++;
                            }

                            Console.WriteLine($"LLM Call for benchmark {benchmark.Key} has started");
                            (var outputString, var tokenCount) = await LlmHelper.InvokeModelAsync(prompt, benchmark.Value as PromptTemplateV1);
                            Console.WriteLine($"LLM Call for benchmark {benchmark.Key} has successfully ended");
                            lock (lockObj)
                            {
                                output.RawOutput[benchmark.Key] = outputString;
                                output.Output[benchmark.Key] = BenchmarkHelper.ConvertToListOfLists(outputString);
                                currentConcurrentRequests--;
                                currentTokenCountInDay += tokenCount;
                            }

                            // If request succeeds break the cycle
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (lockObj)
                        {
                            output.Error.Add(ex.ToString());
                            Console.WriteLine("CONSOLE_EXCEPTION: " + ex.ToString());
                            currentConcurrentRequests--;
                        }
                    }
                }
            });
            await Task.WhenAll(tasks);

            // Generate the JSON output file to be able to pass it to the Python benchmark runner
            var jsonString = JsonSerializer.Serialize(output);
            var jsonPath = "..\\..\\..\\..\\tools\\pcg_benchmark\\pcg_results.json";
            if (File.Exists(jsonPath))
            {
                File.Copy(jsonPath, jsonPath + ".old", true);
            }

            File.WriteAllText(jsonPath, jsonString);
            return output;
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
