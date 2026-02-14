namespace UnitTests
{
    using System.Collections.ObjectModel;
    using System.Text.Json;

    using GeneratorViewModel;

    public class PromptUserDataTests
    {
        // Default values

        [Fact]
        public void GeneralElements_Default_IsNotNull()
        {
            var data = new PromptUserData();

            Assert.NotNull(data.GeneralElements);
        }

        [Fact]
        public void MapTileOptions_Default_IsEmptyCollection()
        {
            var data = new PromptUserData();

            Assert.NotNull(data.MapTileOptions);
            Assert.Empty(data.MapTileOptions);
        }

        [Fact]
        public void MapConstraints_Default_IsNotNull()
        {
            var data = new PromptUserData();

            Assert.NotNull(data.MapConstraints);
        }

        // GetJsonSchema

        [Fact]
        public void GetJsonSchema_ReturnsValidJson()
        {
            var schema = PromptUserData.GetJsonSchema();

            var doc = JsonDocument.Parse(schema);
            Assert.NotNull(doc);
        }

        [Fact]
        public void GetJsonSchema_ContainsGeneralElementsProperty()
        {
            var schema = PromptUserData.GetJsonSchema();

            Assert.Contains("GeneralElements", schema);
        }

        [Fact]
        public void GetJsonSchema_ContainsMapTileOptionsProperty()
        {
            var schema = PromptUserData.GetJsonSchema();

            Assert.Contains("MapTileOptions", schema);
        }

        [Fact]
        public void GetJsonSchema_ContainsMapConstraintsProperty()
        {
            var schema = PromptUserData.GetJsonSchema();

            Assert.Contains("MapConstraints", schema);
        }

        [Fact]
        public void GetJsonSchema_RequiresAllTopLevelProperties()
        {
            var schema = PromptUserData.GetJsonSchema();
            var doc = JsonDocument.Parse(schema);
            var required = doc.RootElement.GetProperty("required");

            var requiredItems = new List<string>();
            foreach (var item in required.EnumerateArray())
            {
                requiredItems.Add(item.GetString()!);
            }

            Assert.Contains("GeneralElements", requiredItems);
            Assert.Contains("MapTileOptions", requiredItems);
            Assert.Contains("MapConstraints", requiredItems);
        }

        [Fact]
        public void GetJsonSchema_DisallowsAdditionalProperties()
        {
            var schema = PromptUserData.GetJsonSchema();
            var doc = JsonDocument.Parse(schema);

            Assert.False(doc.RootElement.GetProperty("additionalProperties").GetBoolean());
        }

        // GetMapResponseJsonSchema

        [Fact]
        public void GetMapResponseJsonSchema_ReturnsValidJson()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "W" },
                new MapTile { TileCharacter = "." },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(5, 5, tiles);

            var doc = JsonDocument.Parse(schema);
            Assert.NotNull(doc);
        }

        [Fact]
        public void GetMapResponseJsonSchema_ContainsMapGridProperty()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "X" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(3, 3, tiles);

            Assert.Contains("mapGrid", schema);
        }

        [Fact]
        public void GetMapResponseJsonSchema_RequiresMapGrid()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "X" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(3, 3, tiles);
            var doc = JsonDocument.Parse(schema);
            var required = doc.RootElement.GetProperty("required");

            Assert.Equal("mapGrid", required[0].GetString());
        }

        [Fact]
        public void GetMapResponseJsonSchema_PatternContainsTileCharacters()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "W" },
                new MapTile { TileCharacter = "." },
                new MapTile { TileCharacter = "P" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(10, 8, tiles);

            Assert.Contains("W", schema);
            Assert.Contains("P", schema);
        }

        [Fact]
        public void GetMapResponseJsonSchema_PatternContainsWidthQuantifier()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "X" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(7, 4, tiles);

            Assert.Contains("{7}", schema);
        }

        [Fact]
        public void GetMapResponseJsonSchema_PatternContainsHeightMinusOneQuantifier()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "X" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(5, 6, tiles);

            Assert.Contains("{5}", schema);
        }

        [Fact]
        public void GetMapResponseJsonSchema_DisallowsAdditionalProperties()
        {
            var tiles = new ObservableCollection<MapTile>
            {
                new MapTile { TileCharacter = "X" },
            };

            var schema = PromptUserData.GetMapResponseJsonSchema(3, 3, tiles);
            var doc = JsonDocument.Parse(schema);

            Assert.False(doc.RootElement.GetProperty("additionalProperties").GetBoolean());
        }
    }
}
