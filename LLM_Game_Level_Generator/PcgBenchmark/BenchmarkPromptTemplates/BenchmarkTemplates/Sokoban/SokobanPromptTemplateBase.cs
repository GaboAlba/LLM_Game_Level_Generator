
namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Sokoban
{
    using GeneratorViewModel;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;

    public class SokobanPromptTemplateBase : PromptTemplateV1
    {
        public SokobanPromptTemplateBase()
        {}
        protected List<MapTile> GetMapTiles(int height, int width)
        {
            var numberOfCrates = Random.Shared.Next(1, Math.Max(width + 1, height + 1));
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
                    MaximumNumberOfTiles = numberOfCrates,
                },
                new MapTile()
                {
                    TileCharacter = "4",
                    TileName = "Target",
                    TileDescription = "Tiles to which the \"Block\" tile need to be moved to solve the level. There **must** be the EXACT same amount, or the level becomes unsolvable",
                    MinimumNumberOfTiles = numberOfCrates,
                    MaximumNumberOfTiles = numberOfCrates,
                },
            };
        }
    }
}
