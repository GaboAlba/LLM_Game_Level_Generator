namespace UnitTests
{
    using LLMPromptProcessor.PromptTemplates;

    public class OptimizerPromptTemplateBaseTests
    {
        private static OptimizerPromptTemplateBase CreateValidInstance(string filePath = "test-optimizer.hbs")
        {
            return new OptimizerPromptTemplateBase(filePath)
            {
                LevelDescription = "Optimizer test level",
                Tiles = "X|Wall|Solid wall|0|10",
            };
        }

        // Constructor

        [Fact]
        public void Constructor_WithFilePath_SetsTemplateFilePath()
        {
            var template = CreateValidInstance("custom-optimizer.hbs");

            Assert.Equal("custom-optimizer.hbs", template.TemplateFilePath);
        }

        // Interface implementation

        [Fact]
        public void ImplementsIPromptTemplate()
        {
            var template = CreateValidInstance();

            Assert.IsAssignableFrom<IPromptTemplate>(template);
        }

        // Default values

        [Fact]
        public void GameName_Default_IsNotProvided()
        {
            var template = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", template.GameName);
        }

        [Fact]
        public void GameDescription_Default_IsNotProvided()
        {
            var template = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", template.GameDescription);
        }

        [Fact]
        public void LevelName_Default_IsNotProvided()
        {
            var template = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", template.LevelName);
        }

        [Fact]
        public void CustomConstraints_Default_IsEmptyString()
        {
            var template = CreateValidInstance();

            Assert.Equal(string.Empty, template.CustomConstraints);
        }

        // Required properties

        [Fact]
        public void RequiredProperties_WhenSet_ReturnCorrectValues()
        {
            var template = CreateValidInstance();

            Assert.Equal("Optimizer test level", template.LevelDescription);
            Assert.Equal("X|Wall|Solid wall|0|10", template.Tiles);
        }

        // Optional properties can be overridden

        [Fact]
        public void OptionalProperties_WhenSet_ReturnOverriddenValues()
        {
            var template = CreateValidInstance();
            template.GameName = "My Game";
            template.GameDescription = "A puzzle game";
            template.LevelName = "Stage 1";
            template.CustomConstraints = "Must have exit";

            Assert.Equal("My Game", template.GameName);
            Assert.Equal("A puzzle game", template.GameDescription);
            Assert.Equal("Stage 1", template.LevelName);
            Assert.Equal("Must have exit", template.CustomConstraints);
        }
    }
}
