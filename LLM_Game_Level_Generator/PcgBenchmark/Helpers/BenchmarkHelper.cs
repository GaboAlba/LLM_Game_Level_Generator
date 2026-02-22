namespace PcgBenchmark.Helpers
{
    using LLMPromptProcessor.PromptTemplates;

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
            // Constants Definition
            const string BinaryPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path10.json";
            const string BinaryPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path20.json";
            const string BinaryPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path30.json";
            const string BinaryPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path40.json";
            const string BinaryPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path50.json";
            const string BinaryPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path60.json";
            const string BinaryPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path70.json";
            const string BinaryPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Binary\\Variants\\Path80.json";

            const string DDavePathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_10_6_Diamonds_4.json";
            const string DDavePathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_0_End_16_10_Diamonds_12.json";
            const string DDavePathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_0_6_End_3_4_Diamonds_3.json";
            const string DDavePathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_10_4_End_6_7_Diamonds_4.json";
            const string DDavePathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_13_10_End_7_0_Diamonds_9.json";
            const string DDavePathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_3_5_End_1_0_Diamonds_6.json";
            const string DDavePathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_5_5_End_6_6_Diamonds_5.json";
            const string DDavePathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\DangerousDave\\Variants\\Start_9_10_End_3_10_Diamonds_7.json";

            const string LoderunnerPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_20_Rope_30.json";
            const string LoderunnerPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_5_Rope_40.json";
            const string LoderunnerPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_120_Rope_0.json";
            const string LoderunnerPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_22_Rope_13.json";
            const string LoderunnerPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_45_Rope_12.json";
            const string LoderunnerPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_50_Rope_10.json";
            const string LoderunnerPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_60_Rope_90.json";
            const string LoderunnerPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\LodeRunnerTile\\Variants\\Ladder_67_Rope_67.json";

            const string MDungeonsPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_3_Length_20.json";
            const string MDungeonsPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_24.json";
            const string MDungeonsPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_7_Length_30.json";
            const string MDungeonsPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_5_Length_28.json";
            const string MDungeonsPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_10_Length_40.json";
            const string MDungeonsPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_2_Length_18.json";
            const string MDungeonsPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_8_Length_35.json";
            const string MDungeonsPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\MiniDungeons\\Variants\\Treasure_6_Length_28.json";

            const string SokobanPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_4.json";
            const string SokobanPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_2.json";
            const string SokobanPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_3.json";
            const string SokobanPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_5.json";
            const string SokobanPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_6.json";
            const string SokobanPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_8.json";
            const string SokobanPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_9.json";
            const string SokobanPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Sokoban\\Variants\\Crates_10.json";

            const string SuperMarioPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_15_Coins_8.json";
            const string SuperMarioPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_3_Jumps_10_Coins_5.json";
            const string SuperMarioPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_5_Jumps_12_Coins_8.json";
            const string SuperMarioPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_7_Jumps_18_Coins_10.json";
            const string SuperMarioPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_2_Jumps_8_Coins_4.json";
            const string SuperMarioPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_6_Jumps_15_Coins_12.json";
            const string SuperMarioPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_4_Jumps_20_Coins_6.json";
            const string SuperMarioPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\SuperMarioBrosTile\\Variants\\Enemies_8_Jumps_22_Coins_10.json";

            const string ZeldaPathZero = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_5_KDD_3.json";
            const string ZeldaPathOne = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_7_KDD_5.json";
            const string ZeldaPathTwo = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_9_KDD_7.json";
            const string ZeldaPathThree = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_11_KDD_9.json";
            const string ZeldaPathFour = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_14_KDD_11.json";
            const string ZeldaPathFive = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_17_KDD_14.json";
            const string ZeldaPathSix = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_20_KDD_16.json";
            const string ZeldaPathSeven = ".\\BenchmarkPromptTemplates\\BenchmarkTemplates\\Zelda\\Variants\\PKD_23_KDD_18.json";

            return new Dictionary<string, object>()
            {
                // ----------Binary variations----------
                // Normal
                { "binary-v0-0", new BinaryV0PromptTemplate(BinaryPathZero)},
                { "binary-v0-1", new BinaryV0PromptTemplate(BinaryPathOne)},
                { "binary-v0-2", new BinaryV0PromptTemplate(BinaryPathTwo)},
                { "binary-v0-3", new BinaryV0PromptTemplate(BinaryPathThree)},
                { "binary-v0-4", new BinaryV0PromptTemplate(BinaryPathFour)},
                { "binary-v0-5", new BinaryV0PromptTemplate(BinaryPathFive)},
                { "binary-v0-6", new BinaryV0PromptTemplate(BinaryPathSix)},
                { "binary-v0-7", new BinaryV0PromptTemplate(BinaryPathSeven)},

                // Large
                { "binary-large-v0-0", new BinaryV0LargePromptTemplate(BinaryPathZero)},
                { "binary-large-v0-1", new BinaryV0LargePromptTemplate(BinaryPathOne)},
                { "binary-large-v0-2", new BinaryV0LargePromptTemplate(BinaryPathTwo)},
                { "binary-large-v0-3", new BinaryV0LargePromptTemplate(BinaryPathThree)},
                { "binary-large-v0-4", new BinaryV0LargePromptTemplate(BinaryPathFour)},
                { "binary-large-v0-5", new BinaryV0LargePromptTemplate(BinaryPathFive)},
                { "binary-large-v0-6", new BinaryV0LargePromptTemplate(BinaryPathSix)},
                { "binary-large-v0-7", new BinaryV0LargePromptTemplate(BinaryPathSeven)},

                // Wide
                { "binary-wide-v0-0", new BinaryV0WidePromptTemplate(BinaryPathZero)},
                { "binary-wide-v0-1", new BinaryV0WidePromptTemplate(BinaryPathOne)},
                { "binary-wide-v0-2", new BinaryV0WidePromptTemplate(BinaryPathTwo)},
                { "binary-wide-v0-3", new BinaryV0WidePromptTemplate(BinaryPathThree)},
                { "binary-wide-v0-4", new BinaryV0WidePromptTemplate(BinaryPathFour)},
                { "binary-wide-v0-5", new BinaryV0WidePromptTemplate(BinaryPathFive)},
                { "binary-wide-v0-6", new BinaryV0WidePromptTemplate(BinaryPathSix)},
                { "binary-wide-v0-7", new BinaryV0WidePromptTemplate(BinaryPathSeven)},

                // ----------Dangerous Dave variations----------
                // Normal
                { "ddave-v0-0", new DDaveV0PromptTemplate(DDavePathZero)},
                { "ddave-v0-1", new DDaveV0PromptTemplate(DDavePathOne)},
                { "ddave-v0-2", new DDaveV0PromptTemplate(DDavePathTwo)},
                { "ddave-v0-3", new DDaveV0PromptTemplate(DDavePathThree)},
                { "ddave-v0-4", new DDaveV0PromptTemplate(DDavePathFour)},
                { "ddave-v0-5", new DDaveV0PromptTemplate(DDavePathFive)},
                { "ddave-v0-6", new DDaveV0PromptTemplate(DDavePathSix)},
                { "ddave-v0-7", new DDaveV0PromptTemplate(DDavePathSeven)},

                // Large
                { "ddave-large-v0-0", new DDaveLargeV0PromptTemplate(DDavePathZero)},
                { "ddave-large-v0-1", new DDaveLargeV0PromptTemplate(DDavePathOne)},
                { "ddave-large-v0-2", new DDaveLargeV0PromptTemplate(DDavePathTwo)},
                { "ddave-large-v0-3", new DDaveLargeV0PromptTemplate(DDavePathThree)},
                { "ddave-large-v0-4", new DDaveLargeV0PromptTemplate(DDavePathFour)},
                { "ddave-large-v0-5", new DDaveLargeV0PromptTemplate(DDavePathFive)},
                { "ddave-large-v0-6", new DDaveLargeV0PromptTemplate(DDavePathSix)},
                { "ddave-large-v0-7", new DDaveLargeV0PromptTemplate(DDavePathSeven)},

                // Complex
                { "ddave-complex-v0-0", new DDaveComplexV0PromptTemplate(DDavePathZero)},
                { "ddave-complex-v0-1", new DDaveComplexV0PromptTemplate(DDavePathOne)},
                { "ddave-complex-v0-2", new DDaveComplexV0PromptTemplate(DDavePathTwo)},
                { "ddave-complex-v0-3", new DDaveComplexV0PromptTemplate(DDavePathThree)},
                { "ddave-complex-v0-4", new DDaveComplexV0PromptTemplate(DDavePathFour)},
                { "ddave-complex-v0-5", new DDaveComplexV0PromptTemplate(DDavePathFive)},
                { "ddave-complex-v0-6", new DDaveComplexV0PromptTemplate(DDavePathSix)},
                { "ddave-complex-v0-7", new DDaveComplexV0PromptTemplate(DDavePathSeven)},

                // ----------Lode Runner variations----------
                // Normal
                { "loderunner-v0-0", new LodeRunnerV0PromptTemplate(LoderunnerPathZero)},
                { "loderunner-v0-1", new LodeRunnerV0PromptTemplate(LoderunnerPathOne)},
                { "loderunner-v0-2", new LodeRunnerV0PromptTemplate(LoderunnerPathTwo)},
                { "loderunner-v0-3", new LodeRunnerV0PromptTemplate(LoderunnerPathThree)},
                { "loderunner-v0-4", new LodeRunnerV0PromptTemplate(LoderunnerPathFour)},
                { "loderunner-v0-5", new LodeRunnerV0PromptTemplate(LoderunnerPathFive)},
                { "loderunner-v0-6", new LodeRunnerV0PromptTemplate(LoderunnerPathSix)},
                { "loderunner-v0-7", new LodeRunnerV0PromptTemplate(LoderunnerPathSeven)},

                // Enemies
                { "loderunner-enemies-v0-0", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathZero)},
                { "loderunner-enemies-v0-1", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathOne)},
                { "loderunner-enemies-v0-2", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathTwo)},
                { "loderunner-enemies-v0-3", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathThree)},
                { "loderunner-enemies-v0-4", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathFour)},
                { "loderunner-enemies-v0-5", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathFive)},
                { "loderunner-enemies-v0-6", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathSix)},
                { "loderunner-enemies-v0-7", new LodeRunnerEnemiesV0PromptTemplate(LoderunnerPathSeven)},

                // Gold
                { "loderunner-gold-v0-0", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathZero)},
                { "loderunner-gold-v0-1", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathOne)},
                { "loderunner-gold-v0-2", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathTwo)},
                { "loderunner-gold-v0-3", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathThree)},
                { "loderunner-gold-v0-4", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathFour)},
                { "loderunner-gold-v0-5", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathFive)},
                { "loderunner-gold-v0-6", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathSix)},
                { "loderunner-gold-v0-7", new LodeRunnerGoldV0PromptTemplate(LoderunnerPathSeven)},

                // ----------Mini Dungeons variations----------
                // Normal
                { "mdungeons-v0-1", new MiniDungeonsV0PromptTemplate(MDungeonsPathZero)},
                { "mdungeons-v0-2", new MiniDungeonsV0PromptTemplate(MDungeonsPathOne)},
                { "mdungeons-v0-3", new MiniDungeonsV0PromptTemplate(MDungeonsPathTwo)},
                { "mdungeons-v0-0", new MiniDungeonsV0PromptTemplate(MDungeonsPathThree)},
                { "mdungeons-v0-4", new MiniDungeonsV0PromptTemplate(MDungeonsPathFour)},
                { "mdungeons-v0-5", new MiniDungeonsV0PromptTemplate(MDungeonsPathFive)},
                { "mdungeons-v0-6", new MiniDungeonsV0PromptTemplate(MDungeonsPathSix)},
                { "mdungeons-v0-7", new MiniDungeonsV0PromptTemplate(MDungeonsPathSeven)},

                // Enemies
                { "mdungeons-enemies-v0-0", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathZero)},
                { "mdungeons-enemies-v0-1", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathOne)},
                { "mdungeons-enemies-v0-2", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathTwo)},
                { "mdungeons-enemies-v0-3", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathThree)},
                { "mdungeons-enemies-v0-4", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathFour)},
                { "mdungeons-enemies-v0-5", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathFive)},
                { "mdungeons-enemies-v0-6", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathSix)},
                { "mdungeons-enemies-v0-7", new MiniDungeonsEnemiesV0PromptTemplate(MDungeonsPathSeven)},

                // Large
                { "mdungeons-large-v0-0", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathZero)},
                { "mdungeons-large-v0-1", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathOne)},
                { "mdungeons-large-v0-2", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathTwo)},
                { "mdungeons-large-v0-3", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathThree)},
                { "mdungeons-large-v0-4", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathFour)},
                { "mdungeons-large-v0-5", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathFive)},
                { "mdungeons-large-v0-6", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathSix)},
                { "mdungeons-large-v0-7", new MiniDungeonsLargeV0PromptTemplate(MDungeonsPathSeven)},

                // ----------Sokoban variations----------
                // Normal
                { "sokoban-v0-0", new SokobanV0PromptTemplate(SokobanPathZero)},
                { "sokoban-v0-1", new SokobanV0PromptTemplate(SokobanPathOne)},
                { "sokoban-v0-2", new SokobanV0PromptTemplate(SokobanPathTwo)},
                { "sokoban-v0-3", new SokobanV0PromptTemplate(SokobanPathThree)},
                { "sokoban-v0-4", new SokobanV0PromptTemplate(SokobanPathFour)},
                { "sokoban-v0-5", new SokobanV0PromptTemplate(SokobanPathFive)},
                { "sokoban-v0-6", new SokobanV0PromptTemplate(SokobanPathSix)},
                { "sokoban-v0-7", new SokobanV0PromptTemplate(SokobanPathSeven)},

                // Complex
                { "sokoban-complex-v0-0", new SokobanComplexV0PromptTemplate(SokobanPathZero)},
                { "sokoban-complex-v0-1", new SokobanComplexV0PromptTemplate(SokobanPathOne)},
                { "sokoban-complex-v0-2", new SokobanComplexV0PromptTemplate(SokobanPathTwo)},
                { "sokoban-complex-v0-3", new SokobanComplexV0PromptTemplate(SokobanPathThree)},
                { "sokoban-complex-v0-4", new SokobanComplexV0PromptTemplate(SokobanPathFour)},
                { "sokoban-complex-v0-5", new SokobanComplexV0PromptTemplate(SokobanPathFive)},
                { "sokoban-complex-v0-6", new SokobanComplexV0PromptTemplate(SokobanPathSix)},
                { "sokoban-complex-v0-7", new SokobanComplexV0PromptTemplate(SokobanPathSeven)},

                // Large
                { "sokoban-large-v0-0", new SokobanLargeV0PromptTemplate(SokobanPathZero)},
                { "sokoban-large-v0-1", new SokobanLargeV0PromptTemplate(SokobanPathOne)},
                { "sokoban-large-v0-2", new SokobanLargeV0PromptTemplate(SokobanPathTwo)},
                { "sokoban-large-v0-3", new SokobanLargeV0PromptTemplate(SokobanPathThree)},
                { "sokoban-large-v0-4", new SokobanLargeV0PromptTemplate(SokobanPathFour)},
                { "sokoban-large-v0-5", new SokobanLargeV0PromptTemplate(SokobanPathFive)},
                { "sokoban-large-v0-6", new SokobanLargeV0PromptTemplate(SokobanPathSix)},
                { "sokoban-large-v0-7", new SokobanLargeV0PromptTemplate(SokobanPathSeven)},

                // ----------Super Mario Bros. variations----------
                // Normal
                { "mario-v0-0", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathZero)},
                { "mario-v0-1", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathOne)},
                { "mario-v0-2", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathTwo)},
                { "mario-v0-3", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathThree)},
                { "mario-v0-4", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathFour)},
                { "mario-v0-5", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathFive)},
                { "mario-v0-6", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathSix)},
                { "mario-v0-7", new SuperMarioBrosTileV0PromptTemplate(SuperMarioPathSeven)},

                // Medium
                { "mario-medium-v0-0", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathZero)},
                { "mario-medium-v0-1", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathOne)},
                { "mario-medium-v0-2", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathTwo)},
                { "mario-medium-v0-3", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathThree)},
                { "mario-medium-v0-4", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathFour)},
                { "mario-medium-v0-5", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathFive)},
                { "mario-medium-v0-6", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathSix)},
                { "mario-medium-v0-7", new SuperMarioBrosTileMediumV0PromptTemplate(SuperMarioPathSeven)},

                // Small
                { "mario-small-v0-0", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathZero)},
                { "mario-small-v0-1", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathOne)},
                { "mario-small-v0-2", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathTwo)},
                { "mario-small-v0-3", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathThree)},
                { "mario-small-v0-4", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathFour)},
                { "mario-small-v0-5", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathFive)},
                { "mario-small-v0-6", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathSix)},
                { "mario-small-v0-7", new SuperMarioBrosTileSmallV0PromptTemplate(SuperMarioPathSeven)},

                // ----------Zelda variations----------
                // Normal
                { "zelda-v0-0", new ZeldaV0PromptTemplate(ZeldaPathZero)},
                { "zelda-v0-1", new ZeldaV0PromptTemplate(ZeldaPathOne)},
                { "zelda-v0-2", new ZeldaV0PromptTemplate(ZeldaPathTwo)},
                { "zelda-v0-3", new ZeldaV0PromptTemplate(ZeldaPathThree)},
                { "zelda-v0-4", new ZeldaV0PromptTemplate(ZeldaPathFour)},
                { "zelda-v0-5", new ZeldaV0PromptTemplate(ZeldaPathFive)},
                { "zelda-v0-6", new ZeldaV0PromptTemplate(ZeldaPathSix)},
                { "zelda-v0-7", new ZeldaV0PromptTemplate(ZeldaPathSeven)},

                // Enemies
                { "zelda-enemies-v0-0", new ZeldaEnemiesV0PromptTemplate(ZeldaPathZero)},
                { "zelda-enemies-v0-1", new ZeldaEnemiesV0PromptTemplate(ZeldaPathOne)},
                { "zelda-enemies-v0-2", new ZeldaEnemiesV0PromptTemplate(ZeldaPathTwo)},
                { "zelda-enemies-v0-3", new ZeldaEnemiesV0PromptTemplate(ZeldaPathThree)},
                { "zelda-enemies-v0-4", new ZeldaEnemiesV0PromptTemplate(ZeldaPathFour)},
                { "zelda-enemies-v0-5", new ZeldaEnemiesV0PromptTemplate(ZeldaPathFive)},
                { "zelda-enemies-v0-6", new ZeldaEnemiesV0PromptTemplate(ZeldaPathSix)},
                { "zelda-enemies-v0-7", new ZeldaEnemiesV0PromptTemplate(ZeldaPathSeven)},

                // Large
                { "zelda-large-v0-0", new ZeldaLargeV0PromptTemplate(ZeldaPathZero)},
                { "zelda-large-v0-1", new ZeldaLargeV0PromptTemplate(ZeldaPathOne)},
                { "zelda-large-v0-2", new ZeldaLargeV0PromptTemplate(ZeldaPathTwo)},
                { "zelda-large-v0-3", new ZeldaLargeV0PromptTemplate(ZeldaPathThree)},
                { "zelda-large-v0-4", new ZeldaLargeV0PromptTemplate(ZeldaPathFour)},
                { "zelda-large-v0-5", new ZeldaLargeV0PromptTemplate(ZeldaPathFive)},
                { "zelda-large-v0-6", new ZeldaLargeV0PromptTemplate(ZeldaPathSix)},
                { "zelda-large-v0-7", new ZeldaLargeV0PromptTemplate(ZeldaPathSeven)},
            };
        }

        private static Dictionary<string, object> GetKeyValuePairs(string argument, Dictionary<string, object> currentDict)
        {
            return currentDict.Where(kvp => kvp.Key.StartsWith(argument)).ToDictionary<string, object>();
        }
    }
}
