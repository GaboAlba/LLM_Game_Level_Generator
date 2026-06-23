namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.DangerousDave
{
    using GeneratorViewModel;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;

    public class DDavePromptTemplateBase : PromptTemplateV1
    {
        protected Dictionary<string, Dictionary<string, int>> Ranges;

        public DDavePromptTemplateBase(string width, string height)
        {
            this.Width = width;
            this.Height = height;
            this.Ranges = new()
            {
                { "startX", new()
                    {
                        { "min", 0 },
                        { "max", int.Parse(this.Width) },
                    }
                },
                { "startY", new()
                    {
                        { "min", (int.Parse(this.Height) / 2) + 2},
                        { "max", int.Parse(this.Height) },
                    }
                },
                { "endX", new()
                    {
                        { "min",  0},
                        { "max", int.Parse(this.Width) },
                    }
                },
                { "endY", new()
                    {
                        { "min", 0 },
                        { "max", (int.Parse(this.Height) / 2) },
                    }
                },
                {
                    "diamonds", new()
                    {
                        { "min", 0 },
                        { "max", Math.Min(int.Parse(this.Width), int.Parse(this.Height)) },
                    }
                }
            };
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
