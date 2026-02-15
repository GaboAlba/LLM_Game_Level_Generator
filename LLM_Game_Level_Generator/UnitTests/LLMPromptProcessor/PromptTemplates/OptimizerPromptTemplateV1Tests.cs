namespace UnitTests
{
    using LLMPromptProcessor.PromptTemplates;

    public class OptimizerPromptTemplateV1Tests
    {
        private static OptimizerPromptTemplateV1 CreateValidInstance()
        {
            return new OptimizerPromptTemplateV1
            {
                LevelDescription = "Test level",
                Tiles = "X|Wall|Solid wall|0|10",
            };
        }

        [Fact]
        public void Constructor_SetsTemplateFilePathToOptimizerPromptV1()
        {
            var template = CreateValidInstance();

            Assert.Equal("OptimizerPromptV1.hbs", template.TemplateFilePath);
        }

        [Fact]
        public void InheritsFromOptimizerPromptTemplateBase()
        {
            var template = CreateValidInstance();

            Assert.IsAssignableFrom<OptimizerPromptTemplateBase>(template);
        }

        [Fact]
        public void ImplementsIPromptTemplate()
        {
            var template = CreateValidInstance();

            Assert.IsAssignableFrom<IPromptTemplate>(template);
        }

        [Fact]
        public void DefaultValues_InheritedFromBase()
        {
            var template = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", template.GameName);
            Assert.Equal("NOT PROVIDED", template.GameDescription);
            Assert.Equal("NOT PROVIDED", template.LevelName);
            Assert.Equal(string.Empty, template.CustomConstraints);
        }
    }
}
