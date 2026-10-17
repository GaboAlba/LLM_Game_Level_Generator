namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary
{
    using GeneratorViewModel;

    using LLMGenCoreLib.PromptTemplates;

    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class BinaryPromptTemplateBase : PromptTemplateV2
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
                    TileDescription = "Solid impassable wall"
                },
                new MapTile()
                {
                    TileCharacter = "1",
                    TileName = "Floor",
                    TileDescription = "Walkable floor, used to form open rooms/chambers as well as connecting corridors"
                }
            };
        }
    }
}
