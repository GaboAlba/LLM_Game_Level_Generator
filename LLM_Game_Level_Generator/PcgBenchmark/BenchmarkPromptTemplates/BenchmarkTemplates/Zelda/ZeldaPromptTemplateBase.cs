namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Zelda
{
    using GeneratorViewModel;

    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ZeldaPromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// How much the player need to reach the key.
            /// </summary>
            [JsonPropertyName("player_key")]
            public int PlayerKeyDistance { get; set; }

            /// <summary>
            /// Distance that should be in between the Key and the Door
            /// </summary>
            [JsonPropertyName("key_door")]
            public int KeyDoorDistance { get; set; }
        }

        public ControlParameters controlParameters { get; }

        public ZeldaPromptTemplateBase(string jsonPath)
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

        protected List<MapTile> GetMapTiles(int targetEnemies)
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
                    TileName = "Key",
                    TileDescription = "Key needed to unlock the exit door",
                    MinimumNumberOfTiles = 1,
                    MaximumNumberOfTiles = 1,
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Exit Door",
                    TileDescription = "The end of the level",
                    MinimumNumberOfTiles = 1,
                    MaximumNumberOfTiles = 1,
                },
                new MapTile()
                {
                    TileCharacter = "5",
                    TileName = "Enemy",
                    TileDescription = "The end of the level",
                    MinimumNumberOfTiles = targetEnemies - 1,
                    MaximumNumberOfTiles = targetEnemies + 1,
                },
            };
        }
    }
}
