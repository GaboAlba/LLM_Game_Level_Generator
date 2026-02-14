namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.DangerousDave
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class DDavePromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// The X coordinate for the player to start at.
            /// </summary>
            [JsonPropertyName("sx")]
            public int PlayerStartPositionX { get; set; }

            /// <summary>
            /// The Y coordinate for the player to start at.
            /// </summary>
            [JsonPropertyName("sy")]
            public int PlayerStartPositionY { get; set; }

            /// <summary>
            /// The X coordinate for the exit to be located at.
            /// </summary>
            [JsonPropertyName("ex")]
            public int ExitPositionX { get; set; }

            /// <summary>
            /// The Y coordinate for the exit to be located at.
            /// </summary>
            [JsonPropertyName("ey")]
            public int ExitPositionY { get; set; }

            /// <summary>
            /// The quantity of diamond tiles the map should contain.
            /// </summary>
            [JsonPropertyName("diamonds")]
            public int DiamondsCount { get; set; }
        }

        protected List<MapTile> GetMapTiles(int height, int width)
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
                    TileName = "Exit",
                    TileDescription = "The player's exit for this particular level. This is the goal of the level",
                    MinimumNumberOfTiles = 1,
                    MaximumNumberOfTiles = 1,
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Diamond",
                    TileDescription = "", // TODO: Add description
                    MinimumNumberOfTiles = Math.Min(height, width),
                },
                new MapTile()
                {
                    TileCharacter = "5",
                    TileName = "Key",
                    TileDescription = "Mandatory prerequisite to be able to unlock the door",
                    MinimumNumberOfTiles = 1,
                    MaximumNumberOfTiles = 1,
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Spikes",
                    TileDescription = "", // TODO: Add description
                    MaximumNumberOfTiles = Math.Max(height, width) * 2,
                }
            };
        }

    }
}
