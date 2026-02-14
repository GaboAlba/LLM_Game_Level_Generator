namespace UnitTests
{
    using ExternalServices.Contract;
    using ExternalServices.Contract.LLM_Response;

    public class LLMResponseTests
    {
        [Fact]
        public void Constructor_WithRequiredId_SetsId()
        {
            var response = new LLMResponse { Id = "resp-123" };

            Assert.Equal("resp-123", response.Id);
        }

        [Fact]
        public void OptionalProperties_WhenNotSet_DefaultToNull()
        {
            var response = new LLMResponse { Id = "resp-123" };

            Assert.Null(response.Model);
            Assert.Null(response.Error);
            Assert.Null(response.MaxOutputTokens);
            Assert.Null(response.OutputText);
            Assert.Null(response.ReasoningText);
        }

        [Fact]
        public void AllProperties_WhenSet_ReturnCorrectValues()
        {
            var error = new LLMError { Code = "500", Message = "Internal error" };

            var response = new LLMResponse
            {
                Id = "resp-456",
                Model = "gpt-5.2",
                Error = error,
                MaxOutputTokens = 10000,
                OutputText = "Generated output",
                ReasoningText = "Step-by-step reasoning",
            };

            Assert.Equal("resp-456", response.Id);
            Assert.Equal("gpt-5.2", response.Model);
            Assert.Same(error, response.Error);
            Assert.Equal(10000, response.MaxOutputTokens);
            Assert.Equal("Generated output", response.OutputText);
            Assert.Equal("Step-by-step reasoning", response.ReasoningText);
        }
    }
}
