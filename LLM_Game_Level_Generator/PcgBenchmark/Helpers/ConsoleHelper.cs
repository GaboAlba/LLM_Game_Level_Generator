namespace PcgBenchmark.Helpers
{    using LLMPromptProcessor.PromptTemplates;

    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.DangerousDave;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.LodeRunnerTile;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.MiniDungeons;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Sokoban;
    using PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.SuperMarioBrosTile;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class ConsoleHelper
    {
        // if (args[i] == "-benchmarks")
        internal static Dictionary<string, PromptTemplateV1> GetBenchmarksToRun(string arg)
        {
            var dict = GetAllPossibleBenchmarks();

            // Process the benchmark to run
            switch (arg)
            {
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

        private static Dictionary<string, PromptTemplateV1> GetAllPossibleBenchmarks()
        {
            return new Dictionary<string, PromptTemplateV1>()
            {
                // ----------Binary variations----------
                // Normal
                { "binary-v0-0", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path10.json")},
                { "binary-v0-1", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path20.json")},
                { "binary-v0-2", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path30.json")},
                { "binary-v0-3", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path40.json")},
                { "binary-v0-4", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path50.json")},
                { "binary-v0-5", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path60.json")},
                { "binary-v0-6", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path70.json")},
                { "binary-v0-7", new BinaryV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path80.json")},

                // Large
                { "binary-large-v0-0", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path10.json")},
                { "binary-large-v0-1", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path20.json")},
                { "binary-large-v0-2", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path30.json")},
                { "binary-large-v0-3", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path40.json")},
                { "binary-large-v0-4", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path50.json")},
                { "binary-large-v0-5", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path60.json")},
                { "binary-large-v0-6", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path70.json")},
                { "binary-large-v0-7", new BinaryV0LargePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path80.json")},

                // Wide
                { "binary-wide-v0-0", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path10.json")},
                { "binary-wide-v0-1", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path20.json")},
                { "binary-wide-v0-2", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path30.json")},
                { "binary-wide-v0-3", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path40.json")},
                { "binary-wide-v0-4", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path50.json")},
                { "binary-wide-v0-5", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path60.json")},
                { "binary-wide-v0-6", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path70.json")},
                { "binary-wide-v0-7", new BinaryV0WidePromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path80.json")},

                // ----------Dangerous Dave variations----------
                // Normal
                { "ddave-v0-0", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_10_6_Diamonds_4.json")},
                { "ddave-v0-1", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_16_10_Diamonds_12.json")},
                { "ddave-v0-2", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_6_End_3_4_Diamonds_3.json")},
                { "ddave-v0-3", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_10_4_End_6_7_Diamonds_4.json")},
                { "ddave-v0-4", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_13_10_End_7_0_Diamonds_9.json")},
                { "ddave-v0-5", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_3_5_End_1_0_Diamonds_6.json")},
                { "ddave-v0-6", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_5_5_End_6_6_Diamonds_5.json")},
                { "ddave-v0-7", new DDaveV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_9_10_End_3_10_Diamonds_7.json")},

                // Large
                { "ddave-large-v0-0", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_10_6_Diamonds_4.json")},
                { "ddave-large-v0-1", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_16_10_Diamonds_12.json")},
                { "ddave-large-v0-2", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_6_End_3_4_Diamonds_3.json")},
                { "ddave-large-v0-3", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_10_4_End_6_7_Diamonds_4.json")},
                { "ddave-large-v0-4", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_13_10_End_7_0_Diamonds_9.json")},
                { "ddave-large-v0-5", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_3_5_End_1_0_Diamonds_6.json")},
                { "ddave-large-v0-6", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_5_5_End_6_6_Diamonds_5.json")},
                { "ddave-large-v0-7", new DDaveLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_9_10_End_3_10_Diamonds_7.json")},

                // Complex
                { "ddave-complex-v0-0", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_10_6_Diamonds_4.json")},
                { "ddave-complex-v0-1", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_16_10_Diamonds_12.json")},
                { "ddave-complex-v0-2", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_6_End_3_4_Diamonds_3.json")},
                { "ddave-complex-v0-3", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_10_4_End_6_7_Diamonds_4.json")},
                { "ddave-complex-v0-4", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_13_10_End_7_0_Diamonds_9.json")},
                { "ddave-complex-v0-5", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_3_5_End_1_0_Diamonds_6.json")},
                { "ddave-complex-v0-6", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_5_5_End_6_6_Diamonds_5.json")},
                { "ddave-complex-v0-7", new DDaveComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_9_10_End_3_10_Diamonds_7.json")},

                // ----------Lode Runner variations----------
                // Normal
                { "loderunner-v0-0", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_20_Rope_30.json")},
                { "loderunner-v0-1", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_5_Rope_40.json")},
                { "loderunner-v0-2", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_120_Rope_0.json")},
                { "loderunner-v0-3", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_22_Rope_13.json")},
                { "loderunner-v0-4", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_45_Rope_12.json")},
                { "loderunner-v0-5", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_50_Rope_10.json")},
                { "loderunner-v0-6", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_60_Rope_90.json")},
                { "loderunner-v0-7", new LodeRunnerV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_67_Rope_67.json")},

                // Enemies
                { "loderunner-enemies-v0-0", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_20_Rope_30.json")},
                { "loderunner-enemies-v0-1", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_5_Rope_40.json")},
                { "loderunner-enemies-v0-2", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_120_Rope_0.json")},
                { "loderunner-enemies-v0-3", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_22_Rope_13.json")},
                { "loderunner-enemies-v0-4", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_45_Rope_12.json")},
                { "loderunner-enemies-v0-5", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_50_Rope_10.json")},
                { "loderunner-enemies-v0-6", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_60_Rope_90.json")},
                { "loderunner-enemies-v0-7", new LodeRunnerEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_67_Rope_67.json")},

                // Gold
                { "loderunner-gold-v0-0", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_20_Rope_30.json")},
                { "loderunner-gold-v0-1", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_5_Rope_40.json")},
                { "loderunner-gold-v0-2", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_120_Rope_0.json")},
                { "loderunner-gold-v0-3", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_22_Rope_13.json")},
                { "loderunner-gold-v0-4", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_45_Rope_12.json")},
                { "loderunner-gold-v0-5", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_50_Rope_10.json")},
                { "loderunner-gold-v0-6", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_60_Rope_90.json")},
                { "loderunner-gold-v0-7", new LodeRunnerGoldV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_67_Rope_67.json")},

                // ----------Mini Dungeons variations----------
                // Normal
                { "mdungeons-v0-0", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_28.json")},
                { "mdungeons-v0-1", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_3_Length_20.json")},
                { "mdungeons-v0-2", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_24.json")},
                { "mdungeons-v0-3", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_7_Length_30.json")},
                { "mdungeons-v0-4", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_10_Length_40.json")},
                { "mdungeons-v0-5", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_2_Length_18.json")},
                { "mdungeons-v0-6", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_8_Length_35.json")},
                { "mdungeons-v0-7", new MiniDungeonsV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_6_Length_28.json")},

                // Enemies
                { "mdungeons-enemies-v0-0", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_28.json")},
                { "mdungeons-enemies-v0-1", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_3_Length_20.json")},
                { "mdungeons-enemies-v0-2", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_24.json")},
                { "mdungeons-enemies-v0-3", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_7_Length_30.json")},
                { "mdungeons-enemies-v0-4", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_10_Length_40.json")},
                { "mdungeons-enemies-v0-5", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_2_Length_18.json")},
                { "mdungeons-enemies-v0-6", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_8_Length_35.json")},
                { "mdungeons-enemies-v0-7", new MiniDungeonsEnemiesV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_6_Length_28.json")},

                // Large
                { "mdungeons-large-v0-0", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_28.json")},
                { "mdungeons-large-v0-1", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_3_Length_20.json")},
                { "mdungeons-large-v0-2", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_24.json")},
                { "mdungeons-large-v0-3", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_7_Length_30.json")},
                { "mdungeons-large-v0-4", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_10_Length_40.json")},
                { "mdungeons-large-v0-5", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_2_Length_18.json")},
                { "mdungeons-large-v0-6", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_8_Length_35.json")},
                { "mdungeons-large-v0-7", new MiniDungeonsLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_6_Length_28.json")},

                // ----------Sokoban variations----------
                // Normal
                { "sokoban-v0-0", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_4.json")},
                { "sokoban-v0-1", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_2.json")},
                { "sokoban-v0-2", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_3.json")},
                { "sokoban-v0-3", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_5.json")},
                { "sokoban-v0-4", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_6.json")},
                { "sokoban-v0-5", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_8.json")},
                { "sokoban-v0-6", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_9.json")},
                { "sokoban-v0-7", new SokobanV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_10.json")},

                // Complex
                { "sokoban-complex-v0-0", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_4.json")},
                { "sokoban-complex-v0-1", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_2.json")},
                { "sokoban-complex-v0-2", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_3.json")},
                { "sokoban-complex-v0-3", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_5.json")},
                { "sokoban-complex-v0-4", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_6.json")},
                { "sokoban-complex-v0-5", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_8.json")},
                { "sokoban-complex-v0-6", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_9.json")},
                { "sokoban-complex-v0-7", new SokobanComplexV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_10.json")},

                // Large
                { "sokoban-large-v0-0", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_4.json")},
                { "sokoban-large-v0-1", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_2.json")},
                { "sokoban-large-v0-2", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_3.json")},
                { "sokoban-large-v0-3", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_5.json")},
                { "sokoban-large-v0-4", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_6.json")},
                { "sokoban-large-v0-5", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_8.json")},
                { "sokoban-large-v0-6", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_9.json")},
                { "sokoban-large-v0-7", new SokobanLargeV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_10.json")},

                // ----------Super Mario Bros. variations----------
                // Normal
                { "mario-v0-0", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_15_Coins_8.json")},
                { "mario-v0-1", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_3_Jumps_10_Coins_5.json")},
                { "mario-v0-2", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_12_Coins_8.json")},
                { "mario-v0-3", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_7_Jumps_18_Coins_10.json")},
                { "mario-v0-4", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_2_Jumps_8_Coins_4.json")},
                { "mario-v0-5", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_6_Jumps_15_Coins_12.json")},
                { "mario-v0-6", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_4_Jumps_20_Coins_6.json")},
                { "mario-v0-7", new SuperMarioBrosTileV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_8_Jumps_22_Coins_10.json")},

                // Medium
                { "mario-medium-v0-0", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_15_Coins_8.json")},
                { "mario-medium-v0-1", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_3_Jumps_10_Coins_5.json")},
                { "mario-medium-v0-2", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_12_Coins_8.json")},
                { "mario-medium-v0-3", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_7_Jumps_18_Coins_10.json")},
                { "mario-medium-v0-4", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_2_Jumps_8_Coins_4.json")},
                { "mario-medium-v0-5", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_6_Jumps_15_Coins_12.json")},
                { "mario-medium-v0-6", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_4_Jumps_20_Coins_6.json")},
                { "mario-medium-v0-7", new SuperMarioBrosTileMediumV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_8_Jumps_22_Coins_10.json")},

                // Small
                { "mario-small-v0-0", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_15_Coins_8.json")},
                { "mario-small-v0-1", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_3_Jumps_10_Coins_5.json")},
                { "mario-small-v0-2", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_12_Coins_8.json")},
                { "mario-small-v0-3", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_7_Jumps_18_Coins_10.json")},
                { "mario-small-v0-4", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_2_Jumps_8_Coins_4.json")},
                { "mario-small-v0-5", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_6_Jumps_15_Coins_12.json")},
                { "mario-small-v0-6", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_4_Jumps_20_Coins_6.json")},
                { "mario-small-v0-7", new SuperMarioBrosTileSmallV0PromptTemplate(".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_8_Jumps_22_Coins_10.json")},
            };
        }

        private static Dictionary<string, PromptTemplateV1> GetKeyValuePairs(string argument, Dictionary<string, PromptTemplateV1> currentDict)
        {
            return currentDict.Where(kvp => kvp.Key.StartsWith(argument)).ToDictionary<string, PromptTemplateV1>();
        }
    }
}
