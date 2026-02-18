namespace UnitTests
{
    using GeneratorViewModel;

    public class FontPropertiesTests
    {
        [Fact]
        public void OutputLineHeight_Default_IsOne()
        {
            var font = new FontProperties();

            Assert.Equal(1.0, font.OutputLineHeight);
        }

        [Fact]
        public void OutputLineHeight_WhenSet_ReturnsNewValue()
        {
            var font = new FontProperties();

            font.OutputLineHeight = 1.5;

            Assert.Equal(1.5, font.OutputLineHeight);
        }

        [Fact]
        public void OutputLineHeight_WhenSet_RaisesPropertyChanged()
        {
            var font = new FontProperties();
            var raised = false;
            font.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FontProperties.OutputLineHeight))
                    raised = true;
            };

            font.OutputLineHeight = 2.0;

            Assert.True(raised);
        }
    }
}
