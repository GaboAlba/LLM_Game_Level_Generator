namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.Sokoban
{
    using LLMGenCoreLib.PromptTemplates;

    public class SokobanV0PromptTemplate : SokobanPromptTemplateBase
    {
        private const int minimumMovesToSolve = 10;

        public SokobanV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Sokoban";
            this.GameDescription = "Sokoban is an old Japanese block pushing game that inspired a lot of games like Baba is You and game engines like PuzzleScript";
            this.LevelName = "sokoban-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(numberOfCrates: 1));
            this.Width = "5";
            this.Height = "5";
            this.GameType = "Top Down";
            this.GameGenre = "Puzzle";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "None";
            this.CustomConstraints = $"The level solution length **must** be at least {minimumMovesToSolve} steps\n\n" +
                $"The level **must** be solvable by an A* agent";
        }
    }
}
