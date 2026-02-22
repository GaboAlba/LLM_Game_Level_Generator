using LLMPromptProcessor.PromptTemplates;

namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Sokoban
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SokobanPromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// The exact amount of crates that should be created in the map.
            /// </summary>
            [JsonPropertyName("crates")]
            public int CratesCount { get; set; }
        }

        public ControlParameters controlParameters { get; }

        public SokobanPromptTemplateBase(string jsonPath)
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
        protected List<MapTile> GetMapTiles(int numberOfCrates)
        {
            var randInt = Random.Shared.Next(numberOfCrates * 2);
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
                    TileDescription = "Walkable floor"
                },
                new MapTile()
                {
                    TileCharacter = "2",
                    TileName = "Player",
                    TileDescription = "The player's starting position",
                    MinimumNumberOfTiles = 1,
                    MaximumNumberOfTiles = 1,
                },
                new MapTile()
                {
                    TileCharacter = "3",
                    TileName = "Block",
                    TileDescription = "Movable blocks by the player",
                    MinimumNumberOfTiles = numberOfCrates,
                    MaximumNumberOfTiles = numberOfCrates + randInt,
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Target",
                    TileDescription = "Tiles to which the \"Block\" tile need to be moved to solve the level. There **must** be the EXACT same amount, or the level becomes unsolvable",
                    MinimumNumberOfTiles = numberOfCrates,
                    MaximumNumberOfTiles = numberOfCrates + randInt,
                },
            };
        }
    }
}
