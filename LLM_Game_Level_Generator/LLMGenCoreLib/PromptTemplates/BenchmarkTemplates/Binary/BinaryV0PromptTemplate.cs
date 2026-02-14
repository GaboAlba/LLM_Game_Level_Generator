namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.Binary
{
    using LLMGenCoreLib.PromptTemplates;

    public class BinaryV0PromptTemplate : BinaryPromptTemplateBase
    {
        public BinaryV0PromptTemplate()
        {
            this.GameName = "Binary";
            this.GameDescription = "The binary problem is from the PCGRL framework where the goal is to generate a 2D maze of empty and solid tile where it is fully connected and have a long path in it that is more than the a specific distance. The default value is equal to the width + height of the map.";
            this.LevelName = "binary-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.TileList);
            this.Width = "14";
            this.Height = "14";
            this.GameType = "TopDown";
            this.GameGenre = "Maze";
            this.DifficultyLevel = "Easy";
            this.HazardLevel = "None";
            this.CustomConstraints = "The maze **must** have a minimum path length of 28";
        }
    }
}
