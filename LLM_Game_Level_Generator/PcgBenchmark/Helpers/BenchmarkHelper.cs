namespace PcgBenchmark.Helpers
{
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.DangerousDave;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.LodeRunnerTile;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.MiniDungeons;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Sokoban;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.SuperMarioBrosTile;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Zelda;

    using System.Collections.Generic;
    using System.Linq;

    internal static class BenchmarkHelper
    {
        internal static List<List<string>> ConvertToListOfLists(string result)
        {
            List<List<string>> list = [[]];
            char newLine = '\n';
            foreach (var character in result)
            {
                if (character == newLine)
                {
                    list.Add(new List<string>());
                }
                else
                {
                    list[list.Count - 1].Add(character.ToString());
                }
            }

            return list;
        }

        // if (args[i] == "-b")
        internal static Dictionary<string, object> GetBenchmarksToRun(string arg)
        {
            var dict = GetAllPossibleBenchmarks();

            // Process the benchmark to run
            switch (arg)
            {
                case "Debug":
                    return GetKeyValuePairs("binary-v0", dict);
                case "Binary":
                    return GetKeyValuePairs("binary", dict);
                case "Ddave":
                    return GetKeyValuePairs("ddave", dict);
                case "Loderunner":
                    return GetKeyValuePairs("loderunner", dict);
                case "Mdungeons":
                    return GetKeyValuePairs("mdungeons", dict);
                case "Sokoban":
                    return GetKeyValuePairs("sokoban", dict);
                case "Mario":
                    return GetKeyValuePairs("mario", dict);
                case "Zelda":
                    return GetKeyValuePairs("zelda", dict);
                case "All":
                default:
                    return dict;
            }
        }

        internal static Dictionary<string, object> GetAllPossibleBenchmarks()
        {
            return new Dictionary<string, object>()
            {
                // ----------Binary variations----------
                { "binary-v0", new BinaryV0PromptTemplate()},
                { "binary-large-v0", new BinaryV0LargePromptTemplate()},
                { "binary-wide-v0", new BinaryV0WidePromptTemplate()},

                // ----------Dangerous Dave variations----------
                { "ddave-v0", new DDaveV0PromptTemplate()},
                { "ddave-large-v0", new DDaveLargeV0PromptTemplate()},
                { "ddave-complex-v0", new DDaveComplexV0PromptTemplate()},

                // ----------Lode Runner variations----------
                { "loderunner-v0" , new LodeRunnerV0PromptTemplate()},
                { "loderunner-enemies-v0", new LodeRunnerEnemiesV0PromptTemplate()},
                { "loderunner-gold-v0", new LodeRunnerGoldV0PromptTemplate()},

                // ----------Mini Dungeons variations----------
                { "mdungeons-v0", new MiniDungeonsV0PromptTemplate()},
                { "mdungeons-enemies-v0", new MiniDungeonsEnemiesV0PromptTemplate()},
                { "mdungeons-large-v0", new MiniDungeonsLargeV0PromptTemplate()},

                // ----------Sokoban variations----------
                { "sokoban-v0", new SokobanV0PromptTemplate()},
                { "sokoban-complex-v0", new SokobanComplexV0PromptTemplate()},
                { "sokoban-large-v0", new SokobanLargeV0PromptTemplate()},

                // ----------Super Mario Bros. variations----------
                { "mario-v0", new SuperMarioBrosTileV0PromptTemplate()},
                { "mario-medium-v0", new SuperMarioBrosTileMediumV0PromptTemplate()},
                { "mario-small-v0", new SuperMarioBrosTileSmallV0PromptTemplate()},

                // ----------Zelda variations----------
                { "zelda-v0", new ZeldaV0PromptTemplate()},
                { "zelda-enemies-v0", new ZeldaEnemiesV0PromptTemplate()},
                { "zelda-large-v0", new ZeldaLargeV0PromptTemplate()},
            };
        }

        private static Dictionary<string, object> GetKeyValuePairs(string argument, Dictionary<string, object> currentDict)
        {
            return currentDict.Where(kvp => kvp.Key.StartsWith(argument)).ToDictionary<string, object>();
        }
    }
}
