namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary
{
    using GeneratorViewModel;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class BinaryPromptTemplateBase : PromptTemplateV1
    {
        public BinaryPromptTemplateBase()
        {}

        protected List<MapTile> GetMapTiles()
        {
            return new List<MapTile>()
            {
                new MapTile()
                {
                    TileCharacter = "0",
                    TileName = "Wall",
                    TileDescription = "Solid impassable wall for the maze"
                },
                new MapTile()
                {
                    TileCharacter = "1",
                    TileName = "Floor",
                    TileDescription = "Walkable floor for the maze"
                }
            };
        }
    }
}
