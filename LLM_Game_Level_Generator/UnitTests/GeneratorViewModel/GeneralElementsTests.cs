namespace UnitTests
{
    using GeneratorViewModel;

    public class GeneralElementsTests
    {
        // Default values

        [Fact]
        public void GameName_Default_IsEmptyString()
        {
            var elements = new GeneralElements();

            Assert.Equal(string.Empty, elements.GameName);
        }

        [Fact]
        public void GameDescription_Default_IsEmptyString()
        {
            var elements = new GeneralElements();

            Assert.Equal(string.Empty, elements.GameDescription);
        }

        [Fact]
        public void LevelName_Default_IsEmptyString()
        {
            var elements = new GeneralElements();

            Assert.Equal(string.Empty, elements.LevelName);
        }

        [Fact]
        public void LevelDescription_Default_IsEmptyString()
        {
            var elements = new GeneralElements();

            Assert.Equal(string.Empty, elements.LevelDescription);
        }

        // Property setters

        [Fact]
        public void GameName_WhenSet_ReturnsNewValue()
        {
            var elements = new GeneralElements();

            elements.GameName = "My Game";

            Assert.Equal("My Game", elements.GameName);
        }

        [Fact]
        public void GameDescription_WhenSet_ReturnsNewValue()
        {
            var elements = new GeneralElements();

            elements.GameDescription = "A fun game";

            Assert.Equal("A fun game", elements.GameDescription);
        }

        [Fact]
        public void LevelName_WhenSet_ReturnsNewValue()
        {
            var elements = new GeneralElements();

            elements.LevelName = "Level 1";

            Assert.Equal("Level 1", elements.LevelName);
        }

        [Fact]
        public void LevelDescription_WhenSet_ReturnsNewValue()
        {
            var elements = new GeneralElements();

            elements.LevelDescription = "A tricky maze";

            Assert.Equal("A tricky maze", elements.LevelDescription);
        }

        // PropertyChanged notifications

        [Theory]
        [InlineData(nameof(GeneralElements.GameName))]
        [InlineData(nameof(GeneralElements.GameDescription))]
        [InlineData(nameof(GeneralElements.LevelName))]
        [InlineData(nameof(GeneralElements.LevelDescription))]
        public void PropertyChanged_WhenPropertySet_RaisesEvent(string propertyName)
        {
            var elements = new GeneralElements();
            var raised = false;
            elements.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                    raised = true;
            };

            typeof(GeneralElements).GetProperty(propertyName)!.SetValue(elements, "test value");

            Assert.True(raised);
        }

        // CopyFrom

        [Fact]
        public void CopyFrom_WithPopulatedSource_CopiesAllProperties()
        {
            var source = new GeneralElements
            {
                GameName = "Source Game",
                GameDescription = "Source Description",
                LevelName = "Source Level",
                LevelDescription = "Source Level Description",
            };
            var target = new GeneralElements();

            target.CopyFrom(source);

            Assert.Equal("Source Game", target.GameName);
            Assert.Equal("Source Description", target.GameDescription);
            Assert.Equal("Source Level", target.LevelName);
            Assert.Equal("Source Level Description", target.LevelDescription);
        }

        [Fact]
        public void CopyFrom_WithDefaultSource_CopiesDefaultValues()
        {
            var target = new GeneralElements
            {
                GameName = "Existing",
                GameDescription = "Existing",
                LevelName = "Existing",
                LevelDescription = "Existing",
            };
            var source = new GeneralElements();

            target.CopyFrom(source);

            Assert.Equal(string.Empty, target.GameName);
            Assert.Equal(string.Empty, target.GameDescription);
            Assert.Equal(string.Empty, target.LevelName);
            Assert.Equal(string.Empty, target.LevelDescription);
        }
    }
}
