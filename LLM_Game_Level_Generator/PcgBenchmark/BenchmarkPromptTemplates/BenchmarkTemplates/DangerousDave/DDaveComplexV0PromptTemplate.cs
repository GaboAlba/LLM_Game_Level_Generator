namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.DangerousDave
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class DDaveComplexV0PromptTemplate : DDavePromptTemplateBase
    {
        [SetsRequiredMembers]
        public DDaveComplexV0PromptTemplate()
            : base(width: "11", height: "7")
        {
            this.GameName = "Dangerous Dave";
            this.GameDescription = "This is a small discrete version of the DOS game Dangerous Dave similar to the one implemented in the PCGRL Framework. Dangerous dave is a small platformer where you need to get a key avoid spikes and collect diamonds and get to exit.";
            this.LevelName = "ddave-complex-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(int.Parse(this.Height), int.Parse(this.Width)));
            this.GameType = "Platformer";
            this.GameGenre = "Puzzle";
            this.DifficultyLevel = "Easy";
            this.HazardLevel = "Low";
            this.CustomConstraints = $"The player and exit **must** be above a solid tile.\n\n" +
                $"The player's starting position **must** be between: \n " +
                $"- {this.Ranges["startX"]["min"]} and {this.Ranges["startX"]["max"]} in X \n" +
                $"- {this.Ranges["startY"]["min"]} and {this.Ranges["startY"]["max"]} in Y \n" +
                $"Additionally, the map exit position **must** be between \n" +
                $"- {this.Ranges["endX"]["min"]} and {this.Ranges["endX"]["max"]} in X \n" +
                $"- {this.Ranges["endY"]["min"]} and {this.Ranges["endY"]["max"]} in Y.\n" +
                $"The map **must** also contain a minimum of 6 jumps. \n\n " +
                $"Finally, the map **must** contain between {this.Ranges["diamonds"]["min"]} and {this.Ranges["diamonds"]["max"]} diamonds distributed across the map.\n\n" +
                $"The diamonds **must** be reachable.";
        }
    }
}
