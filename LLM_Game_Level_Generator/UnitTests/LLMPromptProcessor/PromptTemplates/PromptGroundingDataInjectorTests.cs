namespace UnitTests
{
    using System.Collections.ObjectModel;

    using GeneratorViewModel;
    using LLMGenCoreLib.PromptTemplates;

    public class PromptGroundingDataInjectorTests
    {
        private static PromptUserData CreateValidPromptUserData()
        {
            return new PromptUserData
            {
                GeneralElements = new GeneralElements
                {
                    GameName = "TestGame",
                    GameDescription = "A test game for unit testing",
                    LevelName = "Level 1",
                    LevelDescription = "A simple test level",
                },
                MapConstraints = new MapConstraints
                {
                    Width = 12,
                    Height = 8,
                    GameType = GameType.TopDown,
                    GameGenre = "Puzzle",
                    DifficultyLevel = DifficultyLevel.Hard,
                    HazardDensity = Density.High,
                    CustomConstraints = "No dead ends allowed",
                },
                MapTileOptions = new ObservableCollection<MapTile>
                {
                    new MapTile
                    {
                        TileCharacter = "W",
                        TileName = "Wall",
                        TileDescription = "Solid wall",
                        MinimumNumberOfTiles = 5,
                        MaximumNumberOfTiles = 30,
                    },
                    new MapTile
                    {
                        TileCharacter = ".",
                        TileName = "Floor",
                        TileDescription = "Open floor",
                        MinimumNumberOfTiles = 10,
                        MaximumNumberOfTiles = null,
                    },
                },
            };
        }

        // CreatePrompt tests

        [Fact]
        public void CreatePrompt_WithValidData_ReturnsNonEmptyString()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Theory]
        [InlineData("TestGame")]
        [InlineData("A test game for unit testing")]
        [InlineData("Level 1")]
        [InlineData("A simple test level")]
        public void CreatePrompt_WithValidData_ContainsGeneralElements(string expectedValue)
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains(expectedValue, result);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("8")]
        [InlineData("TopDown")]
        [InlineData("Hard")]
        [InlineData("High")]
        [InlineData("Puzzle")]
        [InlineData("No dead ends allowed")]
        public void CreatePrompt_WithValidData_ContainsMapConstraints(string expectedValue)
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains(expectedValue, result);
        }

        [Fact]
        public void CreatePrompt_WithValidData_ContainsTileTableHeader()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains("Tile Character|Tile Name|Tile Description|Minimum Number Of Tile|Maximum Number Of Tiles", result);
        }

        [Fact]
        public void CreatePrompt_WithValidData_ContainsTileTableSeparator()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains("---|---|---|---|---", result);
        }

        [Theory]
        [InlineData("W|Wall|Solid wall|5|30")]
        [InlineData(".|Floor|Open floor|10|")]
        public void CreatePrompt_WithValidData_ContainsTileRows(string expectedRow)
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains(expectedRow, result);
        }

        [Fact]
        public void CreatePrompt_WithValidData_ContainsSystemAndUserRoles()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.Contains("\"role\":\"system\"", result);
            Assert.Contains("\"role\":\"user\"", result);
        }

        [Fact]
        public void CreatePrompt_WithDefaultOptionalFields_ContainsEmptyStringDefaults()
        {
            var data = new PromptUserData
            {
                GeneralElements = new GeneralElements
                {
                    LevelDescription = "Minimal level",
                },
                MapConstraints = new MapConstraints
                {
                    Width = 5,
                    Height = 5,
                    GameType = GameType.Platformer,
                    DifficultyLevel = DifficultyLevel.Easy,
                    HazardDensity = Density.None,
                },
                MapTileOptions = new ObservableCollection<MapTile>
                {
                    new MapTile
                    {
                        TileCharacter = "X",
                        TileName = "Block",
                        TileDescription = "A block",
                    },
                },
            };

            var result = PromptGroundingDataInjector.CreatePrompt(data);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        // CreateOptimizerPrompt tests

        [Fact]
        public void CreateOptimizerPrompt_WithValidData_ReturnsNonEmptyString()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Theory]
        [InlineData("TestGame")]
        [InlineData("A test game for unit testing")]
        [InlineData("Level 1")]
        [InlineData("A simple test level")]
        public void CreateOptimizerPrompt_WithValidData_ContainsGeneralElements(string expectedValue)
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.Contains(expectedValue, result);
        }

        [Fact]
        public void CreateOptimizerPrompt_WithValidData_ContainsCustomConstraints()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.Contains("No dead ends allowed", result);
        }

        [Fact]
        public void CreateOptimizerPrompt_WithValidData_ContainsTileData()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.Contains("W|Wall|Solid wall|5|30", result);
            Assert.Contains(".|Floor|Open floor|10|", result);
        }

        [Fact]
        public void CreateOptimizerPrompt_WithValidData_ContainsSystemAndUserRoles()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.Contains("\"role\":\"system\"", result);
            Assert.Contains("\"role\":\"user\"", result);
        }

        [Fact]
        public void CreateOptimizerPrompt_WithValidData_ContainsOptimizationContext()
        {
            var data = CreateValidPromptUserData();

            var result = PromptGroundingDataInjector.CreateOptimizerPrompt(data);

            Assert.Contains("prompt engineer", result);
        }
    }
}
