namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.Binary
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class BinaryPromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// How long the shortest longest path has to be.
            /// </summary>
            [JsonPropertyName("path")]
            public int PathLength { get; set; }
        }

        protected readonly List<MapTile> TileList = new List<MapTile>()
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
