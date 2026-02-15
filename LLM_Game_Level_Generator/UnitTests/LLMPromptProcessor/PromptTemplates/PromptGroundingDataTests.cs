namespace UnitTests
{
    using LLMGenCoreLib.PromptTemplates;
    using LLMPromptProcessor.PromptTemplates;

    public class PromptGroundingDataTests
    {
        private static PromptGroundingData CreateValidInstance(string filePath = "test.hbs")
        {
            return new PromptGroundingData(filePath)
            {
                LevelDescription = "A test level",
                Tiles = "X|Wall|Solid wall|0|10",
                Width = "10",
                Height = "10",
                GameType = "TopDown",
                DifficultyLevel = "Normal",
                HazardLevel = "Low",
            };
        }

        // Constructor

        [Fact]
        public void Constructor_WithFilePath_SetsTemplateFilePath()
        {
            var data = CreateValidInstance("custom-template.hbs");

            Assert.Equal("custom-template.hbs", data.TemplateFilePath);
        }

        // Interface implementation

        [Fact]
        public void ImplementsIPromptTemplate()
        {
            var data = CreateValidInstance();

            Assert.IsAssignableFrom<IPromptTemplate>(data);
        }

        // Default values

        [Fact]
        public void GameName_Default_IsNotProvided()
        {
            var data = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", data.GameName);
        }

        [Fact]
        public void GameDescription_Default_IsNotProvided()
        {
            var data = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", data.GameDescription);
        }

        [Fact]
        public void LevelName_Default_IsNotProvided()
        {
            var data = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", data.LevelName);
        }

        [Fact]
        public void GameGenre_Default_IsNull()
        {
            var data = CreateValidInstance();

            Assert.Null(data.GameGenre);
        }

        [Fact]
        public void CustomConstraints_Default_IsEmptyString()
        {
            var data = CreateValidInstance();

            Assert.Equal(string.Empty, data.CustomConstraints);
        }

        // Required properties

        [Fact]
        public void RequiredProperties_WhenSet_ReturnCorrectValues()
        {
            var data = CreateValidInstance();

            Assert.Equal("A test level", data.LevelDescription);
            Assert.Equal("X|Wall|Solid wall|0|10", data.Tiles);
            Assert.Equal("10", data.Width);
            Assert.Equal("10", data.Height);
            Assert.Equal("TopDown", data.GameType);
            Assert.Equal("Normal", data.DifficultyLevel);
            Assert.Equal("Low", data.HazardLevel);
        }

        // Optional properties can be overridden

        [Fact]
        public void OptionalProperties_WhenSet_ReturnOverriddenValues()
        {
            var data = CreateValidInstance();
            data.GameName = "Test Game";
            data.GameDescription = "A test game description";
            data.LevelName = "Level 1";
            data.GameGenre = "Puzzle";
            data.CustomConstraints = "No dead ends";

            Assert.Equal("Test Game", data.GameName);
            Assert.Equal("A test game description", data.GameDescription);
            Assert.Equal("Level 1", data.LevelName);
            Assert.Equal("Puzzle", data.GameGenre);
            Assert.Equal("No dead ends", data.CustomConstraints);
        }
    }
}
