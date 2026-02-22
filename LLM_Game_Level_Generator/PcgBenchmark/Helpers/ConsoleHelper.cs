namespace PcgBenchmark.Helpers
{
    using LLMPromptProcessor;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;

    internal static class ConsoleHelper
    {
        internal struct ConsoleOutput
        {
            public ConsoleOutput()
            {
                this.BenchmarksToRun = new Dictionary<string, object>();
                this.RawOutput = new Dictionary<string, string>();
                this.Output = new Dictionary<string, List<List<string>>>();
                this.DebugMessage = string.Empty;
                this.Error = string.Empty;
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
            public string Error { get; internal set; }
        }

        internal async static Task<ConsoleOutput?> HandleRequestAsync(string[] args)
        {
            var output = new ConsoleOutput();
            var model = string.Empty;
            if (args.Length == 0)
            {
                output.BenchmarksToRun = BenchmarkHelper.GetAllPossibleBenchmarks();
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
                                return new ConsoleOutput() { Error = $"Model must be one of \n\n {validModels}" };
                            }

                    }
                }
            }

            // Call the model
            var handleBarsEngine = new HandlebarsEngine();
            var lockObj = new object();
            var tasks = output.BenchmarksToRun.Select(async benchmark =>
            {
                var prompt = handleBarsEngine.ParsePrompt(benchmark.Value as PromptTemplateV1);
                if (prompt != null)
                {
                    try
                    {
                        var outputString = await LlmHelper.InvokeModelAsync(prompt);
                        Console.WriteLine($"LLM Call for benchmark {benchmark.Key} has been successful");
                        lock (lockObj)
                        {
                            output.RawOutput[benchmark.Key] = outputString;
                            output.Output[benchmark.Key] = BenchmarkHelper.ConvertToListOfLists(outputString);
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (lockObj)
                        {
                            output.Error = ex.ToString();
                        }
                    }
                }
                
                return output;
            });
            await Task.WhenAll(tasks);

            // Generate the JSON output file to be able to pass it to the Python benchmark runner
            var jsonString = JsonSerializer.Serialize(output);
            var jsonPath = "..\\..\\..\\..\\tools\\pcg_benchmark\\pcg_results.json";
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
                "-m: (Optional) Specifies the model that will be run to generate the levels. Current allowed models are:\n" +
                "   * GPT 4.1: Non-reasoning text model with 1M token limit for the context window\n" +
                "   * GPT 5.2: Reasoning model with a 400k token limit for the context window";
        }
    }
}
