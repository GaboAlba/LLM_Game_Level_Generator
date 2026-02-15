namespace UnitTests
{
    using ExternalServices.Contract.ReasoningProperty;

    public class ReasoningOptionsTests
    {
        // GetEffort tests

        [Theory]
        [InlineData(Effort.Low, "low")]
        [InlineData(Effort.Medium, "medium")]
        [InlineData(Effort.High, "high")]
        public void GetEffort_WithAllValues_ReturnsLowercaseString(Effort effort, string expected)
        {
            var options = new ReasoningOptions { Effort = effort };

            var result = options.GetEffort();

            Assert.Equal(expected, result);
        }

        // GetSummary tests

        [Fact]
        public void GetSummary_WithNone_ReturnsNull()
        {
            var options = new ReasoningOptions { Summary = Summary.None };

            var result = options.GetSummary();

            Assert.Null(result);
        }

        [Theory]
        [InlineData(Summary.Auto, "auto")]
        [InlineData(Summary.Concise, "concise")]
        [InlineData(Summary.Detailed, "detailed")]
        public void GetSummary_WithNonNoneValues_ReturnsLowercaseString(Summary summary, string expected)
        {
            var options = new ReasoningOptions { Summary = summary };

            var result = options.GetSummary();

            Assert.Equal(expected, result);
        }

        // Default values

        [Fact]
        public void DefaultValues_Effort_IsLow()
        {
            var options = new ReasoningOptions();

            Assert.Equal(Effort.Low, options.Effort);
        }

        [Fact]
        public void DefaultValues_Summary_IsNone()
        {
            var options = new ReasoningOptions();

            Assert.Equal(Summary.None, options.Summary);
        }
    }
}
