namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.SuperMarioBrosTile
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class SuperMarioBrosTilePromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// How many enemies should the map have.
            /// </summary>
            [JsonPropertyName("enemies")]
            public int EnemiesCount { get; set; }

            /// <summary>
            /// The amount of jumps that the map should have.
            /// </summary>
            [JsonPropertyName("jumps")]
            public int JumpsCount { get; set; }

            /// <summary>
            /// The amount of coins that the map should hve.
            /// </summary>
            [JsonPropertyName("coins")]
            public int CoinsCount { get; set; }
        }

        protected List<MapTile> GetMapTiles(int targetCoins)
        {
            return new List<MapTile>()
            {
                new MapTile()
                {
                    TileCharacter = "0",
                    TileName = "Empty",
                    TileDescription = "Background tile"
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
                    TileName = "Ladder",
                    TileDescription = "Solid ladder tile",
                },
                new MapTile()
                {
                    TileCharacter = "3",
                    TileName = "Breakable Brick",
                    TileDescription = "Tile that can be destroyed when bumped from beneath and Mario is not in small form",
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Question Mark Block",
                    TileDescription = "Tile that contains a special surprise (power-up) when bumped from beneath",
                },
                new MapTile()
                {
                    TileCharacter = "5",
                    TileName = "Tube",
                    TileDescription = "Used as platforms or \"tunnels\" to other sub-levels",
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Coin",
                    TileDescription = "Collectible coin",
                    MinimumNumberOfTiles = targetCoins - 1,
                    MaximumNumberOfTiles = targetCoins + 1,
                },
                new MapTile()
                {
                    TileCharacter = "7",
                    TileName = "Gomba",
                    TileDescription = "Enemy which dies from jumping on top of them",
                },
                new MapTile()
                {
                    TileCharacter = "8",
                    TileName = "Koopa",
                    TileDescription = "Enemy which when it dies from jumping on top of them, they will proceed to leave a shell which will move horizontally and bounce back and forth on wall",
                },
                new MapTile()
                {
                    TileCharacter = "9",
                    TileName = "Spiny",
                    TileDescription = "Enemy which cannot be killed from jumping on top of them",
                }
            };
        }
    }
}
