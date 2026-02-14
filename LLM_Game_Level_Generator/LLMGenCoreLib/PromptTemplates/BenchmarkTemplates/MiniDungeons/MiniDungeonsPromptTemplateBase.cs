namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.MiniDungeons
{
    using GeneratorViewModel;

    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class MiniDungeonsPromptTemplateBase : PromptTemplateV1
    {
        public class ControlParameters
        {
            /// <summary>
            /// The quantity of treasures that must be collected by the player
            /// </summary>
            [JsonPropertyName("col_treasure")]
            public int TreasuresToCollectAmount { get; set; }

            /// <summary>
            /// The solution length of the level
            /// </summary>
            [JsonPropertyName("solution_length")]
            public int SolutionLength { get; set; }
        }

        protected List<MapTile> GetMapTiles(int minEnemies, int targetTreasures, )
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
                    TileName = "Potion",
                    TileDescription = "Heals the player for 2 HP",
                    MinimumNumberOfTiles = minEnemies,
                },
                new MapTile()
                {
                    TileCharacter = "5",
                    TileName = "Treasure",
                    TileDescription = "These must be collected by the player to unlock the exit",
                    MinimumNumberOfTiles = targetTreasures - 1,
                    MaximumNumberOfTiles = targetTreasures + 1,
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Goblin",
                    TileDescription = "Enemy which damages for 1 HP",
                    MinimumNumberOfTiles = minEnemies/2,
                    MaximumNumberOfTiles = minEnemies,
                },
                new MapTile()
                {
                    TileCharacter = "7",
                    TileName = "Ogre",
                    TileDescription = "Enemy which damages for 2 HP",
                    MinimumNumberOfTiles = minEnemies/2,
                    MaximumNumberOfTiles = minEnemies,
                }
            };
        }
    }
}
