
namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.LodeRunnerTile
{
    using GeneratorViewModel;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;

    public class LodeRunnerPromptTemplateBase : PromptTemplateV1
    {
        public LodeRunnerPromptTemplateBase()
        {}

        protected List<MapTile> GetMapTiles(int minEnemies, int minGold, int width, int height)
        {
            return new List<MapTile>()
            {
                new MapTile()
                {
                    TileCharacter = "0",
                    TileName = "Solid",
                    TileDescription = "Solid tile, both for walls and floors which are impassable"
                },
                new MapTile()
                {
                    TileCharacter = "1",
                    TileName = "Empty",
                    TileDescription = "Background tiles. No effects whatsoever"
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
                    MinimumNumberOfTiles = 0,
                    MaximumNumberOfTiles = (int)(0.2 * height * width),
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Rope",
                    TileDescription = "Allows for horizontal movement over gaps",
                    MinimumNumberOfTiles = 0,
                    MaximumNumberOfTiles = (int)(0.2 * height * width),
                }
            };
        }
    }
}
