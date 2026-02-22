namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.LodeRunnerTile
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class LodeRunnerV0PromptTemplate : LodeRunnerPromptTemplateBase
    {
        [SetsRequiredMembers]
        public LodeRunnerV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Lode Runner";
            this.GameDescription = "This is a simple version of the classic game Lode Runner. Lode Runner is an arcade puzzle platformer where the player can't jump and they need to collect all the gold without being caught by the enemies. The player can move horizontal and climb ladders.";
            this.LevelName = "loderunner-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(minEnemies: 3, minGold: 6, targetLadders: this.controlParameters.LaddersCount, targetRopes: this.controlParameters.RopesCount));
            this.Width = "32";
            this.Height = "21";
            this.GameType = "Platformer";
            this.GameGenre = "Arcade Puzzle";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "Easy";
            this.CustomConstraints = $"The player should be able to explore at least 20% of the map by walking.\n\n" +
                $"The player must be able to reach all the gold locations from the start location.\n\n" +
                $"The horizontal and vertical groups of ladder, ropes, and bricks should fall in the same distribution like the original lode runner.";
        }
    }
}
