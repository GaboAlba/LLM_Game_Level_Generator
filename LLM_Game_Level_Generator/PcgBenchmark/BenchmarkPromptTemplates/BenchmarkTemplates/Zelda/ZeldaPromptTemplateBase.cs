namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Zelda
{
    using GeneratorViewModel;

    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;

    public class ZeldaPromptTemplateBase : PromptTemplateV1
    {
        protected Dictionary<string, int> PlayerKeyDistanceRange;
        protected Dictionary<string, int> KeyDoorDistanceRange;

        public ZeldaPromptTemplateBase(int targetSteps, string widthString, string heightString)
        {
            this.Width = widthString;
            this.Height = heightString;
            var width = int.Parse(this.Width);
            var height = int.Parse(this.Height);

            this.PlayerKeyDistanceRange = new Dictionary<string, int>
            {
                { "min", targetSteps / 2 },
                { "max", width * height / 4 },
            };

            this.KeyDoorDistanceRange = new Dictionary<string, int>
            {
                { "min", targetSteps / 2 },
                { "max", width * height / 4 },
            };
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
