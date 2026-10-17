namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.SuperMarioBrosTile
{
    using GeneratorViewModel;

    using LLMPromptProcessor.PromptTemplates;

    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SuperMarioBrosTilePromptTemplateBase : PromptTemplateV2
    {
        public SuperMarioBrosTilePromptTemplateBase()
        {}

        protected List<MapTile> GetMapTiles(int targetMinCoins, int targetMaxCoins)
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
                    TileDescription = "Used as platforms or \"tunnels\" to other sub-levels. Must form a single solid vertical column exactly 2 tiles wide, occupying the same 2 columns in every row of its height, resting flush on the ground with no floating segments or gaps",
                },
                new MapTile()
                {
                    TileCharacter = "6",
                    TileName = "Coin",
                    TileDescription = "Collectible coin",
                    MinimumNumberOfTiles = targetMinCoins,
                    MaximumNumberOfTiles = targetMaxCoins,
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
