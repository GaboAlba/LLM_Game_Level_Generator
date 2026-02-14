namespace UnitTests
{
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;
    using ExternalServices.Contract.ReasoningProperty;

    public class ResponsesClientTests
    {
        private const string TestApiKey = "test-api-key-for-responses";

        private static ResponsesClient CreateClient() => new(TestApiKey);

        // Constructor and defaults

        [Fact]
        public void Constructor_WithValidApiKey_SetsDefaultModel()
        {
            var client = CreateClient();

            Assert.Equal("gpt-5.2", client.Model);
        }

        [Fact]
        public void Constructor_WithValidApiKey_SetsDefaultHyperparameters()
        {
            var client = CreateClient();

            Assert.Equal(0.3f, client.Temperature);
            Assert.Equal(10000, client.MaxOutputTokens);
            Assert.Equal(0f, client.TopK);
            Assert.Equal(0.9f, client.TopP);
            Assert.Equal(0.3f, client.FrequencyPenalty);
            Assert.Equal(0f, client.PresencePenalty);
        }

        [Fact]
        public void Constructor_WithValidApiKey_ResponseTextFormatIsNull()
        {
            var client = CreateClient();

            Assert.Null(client.ResponseTextFormat);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithNullOrEmptyApiKey_ThrowsArgumentException(string? apiKey)
        {
            Assert.ThrowsAny<ArgumentException>(() => new ResponsesClient(apiKey!));
        }

        // BuildMessages

        [Fact]
        public void BuildMessages_WithValidJsonLines_ReturnsMessages()
        {
            var client = CreateClient();
            var prompt = """{"role":"system","content":"Be helpful"}""" + "\n"
                       + """{"role":"user","content":"Hello"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Equal(2, messages.Count);
            Assert.Equal("system", messages[0].Role);
            Assert.Equal("Be helpful", messages[0].Content);
            Assert.Equal("user", messages[1].Role);
            Assert.Equal("Hello", messages[1].Content);
        }

        [Fact]
        public void BuildMessages_WithSingleMessage_ReturnsSingleMessage()
        {
            var client = CreateClient();
            var prompt = """{"role":"user","content":"Test prompt"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Single(messages);
            Assert.Equal("user", messages[0].Role);
            Assert.Equal("Test prompt", messages[0].Content);
        }

        // BuildRequest

        [Fact]
        public void BuildRequest_WithMessages_SetsModelFromClient()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.Equal("gpt-5.2", request.Model);
        }

        [Fact]
        public void BuildRequest_WithMessages_SetsMaxOutputTokensFromClient()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.Equal(10000, request.MaxOutputTokens);
        }

        [Fact]
        public void BuildRequest_WithMessages_SetsTemperatureFromClient()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.Equal(0.3f, request.Temperature);
        }

        [Fact]
        public void BuildRequest_WithMessages_SetsTopPFromClient()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.Equal(0.9f, request.TopP);
        }

        [Fact]
        public void BuildRequest_WithMessages_SetsReasoningOptions()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.NotNull(request.ReasoningOptions);
            Assert.Equal(Effort.Medium, request.ReasoningOptions.Effort);
            Assert.Equal(Summary.Detailed, request.ReasoningOptions.Summary);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BuildRequest_WithStreamFlag_SetsStreamCorrectly(bool shouldStream)
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "user", Content = "Hello" },
            };

            var request = client.BuildRequest(messages, shouldStream: shouldStream);

            Assert.Equal(shouldStream, request.Stream);
        }

        [Fact]
        public void BuildRequest_WithMessages_InputContainsSameMessages()
        {
            var client = CreateClient();
            var messages = new List<Message>
            {
                new() { Role = "system", Content = "System prompt" },
                new() { Role = "user", Content = "User message" },
            };

            var request = client.BuildRequest(messages, shouldStream: false);

            Assert.Equal(2, request.Input.Count);
            Assert.Equal(messages[0], request.Input[0]);
            Assert.Equal(messages[1], request.Input[1]);
        }
    }
}
