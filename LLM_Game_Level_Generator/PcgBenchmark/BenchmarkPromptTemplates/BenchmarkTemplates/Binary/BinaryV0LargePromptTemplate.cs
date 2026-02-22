namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class BinaryV0LargePromptTemplate : BinaryPromptTemplateBase
    {
        [SetsRequiredMembers]
        public BinaryV0LargePromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Binary";
            this.GameDescription = "The binary problem is from the PCGRL framework where the goal is to generate a 2D maze of only empty and solid tile where it is fully connected and have a long path in it that is more than the a specific distance. The default value is equal to the width + height of the map.";
            this.LevelName = "binary-large-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles());
            this.Width = "28";
            this.Height = "28";
            this.GameType = "TopDown";
            this.GameGenre = "Maze";
            this.DifficultyLevel = "Easy";
            this.HazardLevel = "None";
            this.CustomConstraints = $"The maze **must** have a minimum path length of 56 steps \n\n" +
                $"To pass the controlabiltiy criteria, the longest shortest path **must** be as close as possible to {this.controlParameters.PathLength}\n\n" +
                $"The map **must** be creative and entertaining";
        }
    }
}
