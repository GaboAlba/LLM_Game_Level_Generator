namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class BinaryV0PromptTemplate : BinaryPromptTemplateBase
    {
        [SetsRequiredMembers]
        public BinaryV0PromptTemplate()
        {
            this.GameName = "Binary";
            this.GameDescription = "The binary problem is from the PCGRL framework where the goal is to generate a 2D dungeon of empty and solid tile where it is fully connected and have a long path in it that is more than the a specific distance. The default value is equal to the width + height of the map.";
            this.LevelName = "binary-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles());
            this.Width = "14";
            this.Height = "14";
            this.GameType = "TopDown";
            this.GameGenre = "Dungeon";
            this.DifficultyLevel = "Easy";
            this.HazardLevel = "None";
            this.CustomConstraints = $"The dungeon **must** have a minimum path length of 28 steps\n" +
                $"The map **must** be creative and entertaining \n" +
                $"You have to use the complete level dimensions to create several areas combining open with tight areas without violating the path length\n" +
                $"Do **not** reach the required path length by carving a single one-tile-wide corridor that snakes back and forth across the map (a spiral/serpentine/boustrophedon pattern); this is explicitly forbidden even if it satisfies the path length\n" +
                $"Instead, compose the layout from multiple irrergular open chambers (each at least 3x3 contiguous floor tiles) linked by corridors, and include at least one loop or alternate route where the dimensions allow, so the level reads as a set of rooms rather than one winding hallway";
        }
    }
}
