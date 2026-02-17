using LLMPromptProcessor.PromptTemplates;

namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Binary
{
    using GeneratorViewModel;

    using Microsoft.Extensions.Logging;

    using System.Collections.Generic;
    using System.Text.Json;
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

        protected ControlParameters controlParameters = new();

        public BinaryPromptTemplateBase(string jsonPath)
        {
            try
            {
                var jsonString = File.ReadAllText(jsonPath);
                this.controlParameters = JsonSerializer.Deserialize<ControlParameters>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

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
