namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.DangerousDave
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class DDaveV0PromptTemplate : DDavePromptTemplateBase
    {
        [SetsRequiredMembers]
        public DDaveV0PromptTemplate(string jsonPath)
            : base(jsonPath, "11", "7")
        {
            this.GameName = "Dangerous Dave";
            this.GameDescription = "This is a small discrete version of the DOS game Dangerous Dave similar to the one implemented in the PCGRL Framework. Dangerous dave is a small platformer where you need to get a key avoid spikes and collect diamonds and get to exit.";
            this.LevelName = "ddave-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(int.Parse(this.Height), int.Parse(this.Width)));
            this.GameType = "Platformer";
            this.GameGenre = "Puzzle";
            this.DifficultyLevel = "Easy";
            this.HazardLevel = "Low";
            this.CustomConstraints = $"The player and exit **must** be above a solid tile.\n\n" +
                $"The player's starting position **must** be {this.controlParameters.PlayerStartPositionX} in X, and {this.controlParameters.PlayerStartPositionY} in Y.\n\n" +
                $"Additionally, the map exit position **must** be {this.controlParameters.ExitPositionX} in X, and {this.controlParameters.ExitPositionY} in Y.\n\n" +
                $"The map **must** also contain a minimum of 2 jumps. \n\n " +
                $"Finally, the map **must** contain EXACTLY {this.controlParameters.DiamondsCount} diamonds distributed across the map.\n\n" +
                $"The diamonds **must** be reachable.";
        }
    }
}
