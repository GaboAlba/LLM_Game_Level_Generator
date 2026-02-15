namespace UnitTests
{
    using ExternalServices.Contract;

    public class LLMRequestTests
    {
        private static LLMRequest CreateValidRequest(
            float temperature = 0.5f,
            float topP = 1f,
            float? topK = null)
        {
            var request = new LLMRequest
            {
                Input = [new Message { Role = "user", Content = "test" }],
                Model = "test-model",
                MaxOutputTokens = 1000,
                Temperature = temperature,
            };

            request.TopP = topP;

            if (topK.HasValue)
            {
                request.TopK = topK;
            }

            return request;
        }

        // Temperature validation

        [Theory]
        [InlineData(0f)]
        [InlineData(0.5f)]
        [InlineData(1f)]
        [InlineData(2f)]
        public void Temperature_WithValidValue_SetsTemperature(float temperature)
        {
            var request = CreateValidRequest(temperature: temperature);

            Assert.Equal(temperature, request.Temperature);
        }

        [Theory]
        [InlineData(-0.1f)]
        [InlineData(-1f)]
        [InlineData(2.1f)]
        [InlineData(3f)]
        public void Temperature_WithOutOfRangeValue_ThrowsArgumentException(float temperature)
        {
            Assert.Throws<ArgumentException>(() => CreateValidRequest(temperature: temperature));
        }

        // TopP validation

        [Theory]
        [InlineData(0f)]
        [InlineData(0.5f)]
        [InlineData(1f)]
        public void TopP_WithValidValue_SetsTopP(float topP)
        {
            var request = CreateValidRequest(topP: topP);

            Assert.Equal(topP, request.TopP);
        }

        [Theory]
        [InlineData(-0.1f)]
        [InlineData(1.1f)]
        [InlineData(2f)]
        public void TopP_WithOutOfRangeValue_ThrowsArgumentException(float topP)
        {
            Assert.Throws<ArgumentException>(() => CreateValidRequest(topP: topP));
        }

        // TopK validation

        [Theory]
        [InlineData(1f)]
        [InlineData(5f)]
        [InlineData(10f)]
        public void TopK_WithValidValue_SetsTopK(float topK)
        {
            var request = CreateValidRequest(topK: topK);

            Assert.Equal(topK, request.TopK);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(-1f)]
        [InlineData(11f)]
        [InlineData(100f)]
        public void TopK_WithOutOfRangeValue_ThrowsArgumentException(float topK)
        {
            Assert.Throws<ArgumentException>(() => CreateValidRequest(topK: topK));
        }

        [Fact]
        public void TopK_WhenNotSet_DefaultsToNull()
        {
            var request = CreateValidRequest();

            Assert.Null(request.TopK);
        }

        // Default values

        [Fact]
        public void Stream_Default_IsFalse()
        {
            var request = CreateValidRequest();

            Assert.False(request.Stream);
        }

        [Fact]
        public void Truncation_Default_IsAuto()
        {
            var request = CreateValidRequest();

            Assert.Equal("auto", request.Truncation);
        }

        [Fact]
        public void ReasoningOptions_Default_IsNull()
        {
            var request = CreateValidRequest();

            Assert.Null(request.ReasoningOptions);
        }

        [Fact]
        public void StopSequences_Default_IsNull()
        {
            var request = CreateValidRequest();

            Assert.Null(request.StopSequences);
        }

        [Fact]
        public void MaxToolCalls_Default_IsNull()
        {
            var request = CreateValidRequest();

            Assert.Null(request.MaxToolCalls);
        }

        // Required properties

        [Fact]
        public void RequiredProperties_WithValidValues_SetsCorrectly()
        {
            var messages = new List<Message> { new() { Role = "user", Content = "Hello" } };

            var request = new LLMRequest
            {
                Input = messages,
                Model = "gpt-5.2",
                MaxOutputTokens = 5000,
                Temperature = 0.7f,
            };

            Assert.Equal(messages, request.Input);
            Assert.Equal("gpt-5.2", request.Model);
            Assert.Equal(5000, request.MaxOutputTokens);
            Assert.Equal(0.7f, request.Temperature);
        }
    }
}
