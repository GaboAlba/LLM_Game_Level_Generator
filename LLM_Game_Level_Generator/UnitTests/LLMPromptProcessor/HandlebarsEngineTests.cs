namespace UnitTests
{
    using LLMGenCoreLib.PromptTemplates;
    using LLMPromptProcessor;
    using LLMPromptProcessor.PromptTemplates;

    public class HandlebarsEngineTests
    {
        private readonly HandlebarsEngine _engine = new();

        private static PromptTemplateV1 CreatePromptTemplateV1()
        {
            return new PromptTemplateV1
            {
                LevelDescription = "A dungeon crawl level",
                Tiles = "W|Wall|Solid wall|2|20\nP|Player|Player start|1|1",
                Width = "10",
                Height = "8",
                GameType = "TopDown",
                DifficultyLevel = "Hard",
                HazardLevel = "High",
                GameName = "DungeonQuest",
                GameDescription = "A dungeon exploration game",
                LevelName = "The Dark Cavern",
                GameGenre = "Roguelike",
                CustomConstraints = "Must have exactly one exit",
            };
        }

        private static OptimizerPromptTemplateV1 CreateOptimizerTemplateV1()
        {
            return new OptimizerPromptTemplateV1
            {
                LevelDescription = "An optimized dungeon level",
                Tiles = "W|Wall|Solid wall|2|20",
                GameName = "DungeonQuest",
                GameDescription = "A dungeon exploration game",
                LevelName = "The Dark Cavern",
                CustomConstraints = "Balanced difficulty",
            };
        }

        // PromptV1 template tests

        [Fact]
        public void ParsePrompt_WithPromptTemplateV1_ReturnsNonEmptyString()
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void ParsePrompt_WithPromptTemplateV1_ContainsSystemRole()
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("\"role\":\"system\"", result);
        }

        [Fact]
        public void ParsePrompt_WithPromptTemplateV1_ContainsUserRole()
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("\"role\":\"user\"", result);
        }

        [Theory]
        [InlineData("DungeonQuest")]
        [InlineData("A dungeon exploration game")]
        [InlineData("The Dark Cavern")]
        [InlineData("A dungeon crawl level")]
        [InlineData("TopDown")]
        [InlineData("Hard")]
        [InlineData("High")]
        [InlineData("Roguelike")]
        [InlineData("Must have exactly one exit")]
        public void ParsePrompt_WithPromptTemplateV1_InterpolatesPropertyValues(string expectedValue)
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains(expectedValue, result);
        }

        [Fact]
        public void ParsePrompt_WithPromptTemplateV1_InterpolatesTiles()
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("W|Wall|Solid wall|2|20", result);
        }

        [Fact]
        public void ParsePrompt_WithPromptTemplateV1_InterpolatesWidthAndHeight()
        {
            var template = CreatePromptTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("10", result);
            Assert.Contains("8", result);
        }

        // OptimizerPromptV1 template tests

        [Fact]
        public void ParsePrompt_WithOptimizerTemplate_ReturnsNonEmptyString()
        {
            var template = CreateOptimizerTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void ParsePrompt_WithOptimizerTemplate_ContainsSystemRole()
        {
            var template = CreateOptimizerTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("\"role\":\"system\"", result);
        }

        [Fact]
        public void ParsePrompt_WithOptimizerTemplate_ContainsUserRole()
        {
            var template = CreateOptimizerTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("\"role\":\"user\"", result);
        }

        [Theory]
        [InlineData("DungeonQuest")]
        [InlineData("A dungeon exploration game")]
        [InlineData("The Dark Cavern")]
        [InlineData("An optimized dungeon level")]
        [InlineData("Balanced difficulty")]
        public void ParsePrompt_WithOptimizerTemplate_InterpolatesPropertyValues(string expectedValue)
        {
            var template = CreateOptimizerTemplateV1();

            var result = this._engine.ParsePrompt(template);

            Assert.Contains(expectedValue, result);
        }

        // Default value tests

        [Fact]
        public void ParsePrompt_WithDefaultOptionalValues_InterpolatesNotProvided()
        {
            var template = new PromptTemplateV1
            {
                LevelDescription = "Test level",
                Tiles = "X|Floor|Open space|1|50",
                Width = "5",
                Height = "5",
                GameType = "Platformer",
                DifficultyLevel = "Easy",
                HazardLevel = "Low",
            };

            var result = this._engine.ParsePrompt(template);

            Assert.Contains("NOT PROVIDED", result);
        }
    }
}
