namespace UnitTests
{
    using GeneratorViewModel;

    public class OutputTests
    {
        // Default values

        [Fact]
        public void GeneratedMap_Default_IsPlaceholderText()
        {
            var output = new Output();

            Assert.Equal("The map will be generated here", output.GeneratedMap);
        }

        [Fact]
        public void ReasoningSummary_Default_IsPlaceholderText()
        {
            var output = new Output();

            Assert.Equal("The reasoning chain of thought will be generated here", output.ReasoningSummary);
        }

        // Property setters

        [Fact]
        public void GeneratedMap_WhenSet_ReturnsNewValue()
        {
            var output = new Output();

            output.GeneratedMap = "W.W\n...\nW.W";

            Assert.Equal("W.W\n...\nW.W", output.GeneratedMap);
        }

        [Fact]
        public void ReasoningSummary_WhenSet_ReturnsNewValue()
        {
            var output = new Output();

            output.ReasoningSummary = "Placed walls for boundaries";

            Assert.Equal("Placed walls for boundaries", output.ReasoningSummary);
        }

        // PropertyChanged notifications

        [Fact]
        public void GeneratedMap_WhenSet_RaisesPropertyChanged()
        {
            var output = new Output();
            var raised = false;
            output.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Output.GeneratedMap))
                    raised = true;
            };

            output.GeneratedMap = "new map";

            Assert.True(raised);
        }

        [Fact]
        public void ReasoningSummary_WhenSet_RaisesPropertyChanged()
        {
            var output = new Output();
            var raised = false;
            output.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Output.ReasoningSummary))
                    raised = true;
            };

            output.ReasoningSummary = "new reasoning";

            Assert.True(raised);
        }
    }
}
