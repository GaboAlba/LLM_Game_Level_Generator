namespace UnitTests
{
    using LLMGenCoreLib.PromptTemplates;
    using LLMPromptProcessor.PromptTemplates;

    public class PromptTemplateV1Tests
    {
        private static PromptTemplateV1 CreateValidInstance()
        {
            return new PromptTemplateV1
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

        [Fact]
        public void Constructor_SetsTemplateFilePathToPromptV1()
        {
            var template = CreateValidInstance();

            Assert.Equal("PromptV1.hbs", template.TemplateFilePath);
        }

        [Fact]
        public void InheritsFromPromptGroundingData()
        {
            var template = CreateValidInstance();

            Assert.IsAssignableFrom<PromptGroundingData>(template);
        }

        [Fact]
        public void ImplementsIPromptTemplate()
        {
            var template = CreateValidInstance();

            Assert.IsAssignableFrom<IPromptTemplate>(template);
        }

        [Fact]
        public void DefaultValues_InheritedFromPromptGroundingData()
        {
            var template = CreateValidInstance();

            Assert.Equal("NOT PROVIDED", template.GameName);
            Assert.Equal("NOT PROVIDED", template.GameDescription);
            Assert.Equal("NOT PROVIDED", template.LevelName);
            Assert.Null(template.GameGenre);
            Assert.Equal(string.Empty, template.CustomConstraints);
        }
    }
}
