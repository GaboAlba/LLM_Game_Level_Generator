namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Sokoban
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class SokobanLargeV0PromptTemplate : SokobanPromptTemplateBase
    {
        private const int minimumMovesToSolve = 48;

        [SetsRequiredMembers]
        public SokobanLargeV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Sokoban";
            this.GameDescription = "Sokoban is an old Japanese block pushing game that inspired a lot of games like Baba is You and game engines like PuzzleScript";
            this.LevelName = "sokoban-large-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(numberOfCrates: 1));
            this.Width = "8";
            this.Height = "8";
            this.GameType = "Top Down";
            this.GameGenre = "Puzzle";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "None";
            this.CustomConstraints = $"The level solution length **must** be at least {minimumMovesToSolve} steps\n\n" +
                $"The level **must** be solvable by an A* agent";
        }
    }
}
