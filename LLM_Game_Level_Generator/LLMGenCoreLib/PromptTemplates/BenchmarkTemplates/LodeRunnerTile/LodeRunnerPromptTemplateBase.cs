namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.LodeRunnerTile
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class LodeRunnerPromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// The quantity of ladder tiles the map should contain.
            /// </summary>
            [JsonPropertyName("ladder")]
            public int LaddersCount { get; set; }

            /// <summary>
            /// The quantity of rope tiles the map should contain.
            /// </summary>
            [JsonPropertyName("rope")]
            public int RopesCount { get; set; }
        }

        protected List<MapTile> GetMapTiles(int minEnemies, int minGold, int targetLadders, int targetRopes)
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
                    TileName = "Gold",
                    TileDescription = "Tiles that needs to be collected by the player to win",
                    MinimumNumberOfTiles = minGold,
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Enemy",
                    TileDescription = "Tiles that the player need to avoid while picking up the gold",
                    MinimumNumberOfTiles = minEnemies,
                },
                new MapTile()
                {
                    TileCharacter = "5",
                    TileName = "Ladder",
                    TileDescription = "Tile that lets the player climb vertically",
                    MinimumNumberOfTiles = targetLadders - 1,
                    MaximumNumberOfTiles = targetRopes + 1,
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Rope",
                    TileDescription = "Allows for horizontal movement over air gaps, but at the cost of not being able to jump",
                    MinimumNumberOfTiles = targetRopes - 1,
                    MaximumNumberOfTiles = targetRopes + 1,
                }
            };
        }
    }
}
