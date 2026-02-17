namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.MiniDungeons
{
    using LLMGenCoreLib.PromptTemplates;

    public class MiniDungeonsLargeV0PromptTemplate : MiniDungeonsPromptTemplateBase
    {
        private const int minEnemies = 16;

        public MiniDungeonsLargeV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Mini Dungeons";
            this.GameDescription = "MiniDungeons is a simple turn-based roguelike puzzle game, implemented as a benchmark problem for modeling decision making styles of human players";
            this.LevelName = "mdungeons-large-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(minEnemies: minEnemies, this.controlParameters.TreasuresToCollectAmount));
            this.Width = "16";
            this.Height = "24";
            this.GameType = "Top Down";
            this.GameGenre = "Roguelike Puzzle";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "Easy";
            this.CustomConstraints = $"The level solution length **must** be close to {this.controlParameters.SolutionLength} steps\n\n" +
                $"The wall and floor tiles **must** compose at least 50% of the map" +
                $"The amount of enemies killed on the shortest solution for the level **must** be more than {minEnemies}";
        }
    }
}
