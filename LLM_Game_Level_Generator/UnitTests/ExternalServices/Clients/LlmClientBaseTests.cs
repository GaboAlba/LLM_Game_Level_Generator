namespace UnitTests
{
    using System.Text.Json;

    using ExternalServices.Clients;
    using ExternalServices.Contract;

    public class LlmClientBaseTests
    {
        private class TestLlmClient : LlmClientBase
        {
            protected override ISet<string> AllowedModels => new HashSet<string> { "test-model", "test-model-2" };

            public TestLlmClient(
                string model,
                string apiKey,
                float temperature = 0.3f,
                int maxOutputTokens = 10000,
                string? clientName = null)
                : base(model, apiKey, temperature, maxOutputTokens, clientName: clientName)
            {
            }
        }

        // Constructor validation

        [Fact]
        public void Constructor_WithNullModel_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TestLlmClient(null!, "valid-key"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithNullOrEmptyApiKey_ThrowsArgumentException(string? apiKey)
        {
            Assert.ThrowsAny<ArgumentException>(() => new TestLlmClient("test-model", apiKey!));
        }

        [Fact]
        public void Constructor_WithInvalidModel_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => new TestLlmClient("invalid-model", "valid-key", clientName: "TestClient"));

            Assert.Contains("invalid-model", exception.Message);
            Assert.Contains("TestClient", exception.Message);
        }

        [Fact]
        public void Constructor_WithValidParameters_SetsAllProperties()
        {
            var client = new TestLlmClient(
                model: "test-model",
                apiKey: "test-key",
                temperature: 0.5f,
                maxOutputTokens: 5000);

            Assert.Equal("test-model", client.Model);
            Assert.Equal("test-key", client.ApiKey);
            Assert.Equal(0.5f, client.Temperature);
            Assert.Equal(5000, client.MaxOutputTokens);
        }

        [Fact]
        public void Constructor_WithDefaults_SetsDefaultValues()
        {
            var client = new TestLlmClient("test-model", "test-key");

            Assert.Equal(0.3f, client.Temperature);
            Assert.Equal(10000, client.MaxOutputTokens);
            Assert.Equal(1f, client.TopK);
            Assert.Equal(1f, client.TopP);
            Assert.Equal(0f, client.FrequencyPenalty);
            Assert.Equal(0f, client.PresencePenalty);
        }

        // BuildMessages

        [Fact]
        public void BuildMessages_WithSingleJsonLine_ReturnsSingleMessage()
        {
            var client = new TestLlmClient("test-model", "test-key");
            var prompt = """{"role":"user","content":"Hello"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Single(messages);
            Assert.Equal("user", messages[0].Role);
            Assert.Equal("Hello", messages[0].Content);
        }

        [Fact]
        public void BuildMessages_WithMultipleJsonLines_ReturnsMultipleMessages()
        {
            var client = new TestLlmClient("test-model", "test-key");
            var prompt = """{"role":"system","content":"You are helpful"}""" + "\n"
                       + """{"role":"user","content":"Hello"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Equal(2, messages.Count);
            Assert.Equal("system", messages[0].Role);
            Assert.Equal("You are helpful", messages[0].Content);
            Assert.Equal("user", messages[1].Role);
            Assert.Equal("Hello", messages[1].Content);
        }

        [Fact]
        public void BuildMessages_WithWindowsNewlines_ParsesCorrectly()
        {
            var client = new TestLlmClient("test-model", "test-key");
            var prompt = """{"role":"system","content":"System prompt"}""" + "\r\n"
                       + """{"role":"user","content":"User prompt"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Equal(2, messages.Count);
        }

        [Fact]
        public void BuildMessages_WithInvalidJson_ThrowsJsonException()
        {
            var client = new TestLlmClient("test-model", "test-key");

            Assert.Throws<JsonException>(() => client.BuildMessages("not valid json"));
        }

        [Fact]
        public void BuildMessages_SkipsEmptyLines()
        {
            var client = new TestLlmClient("test-model", "test-key");
            var prompt = """{"role":"user","content":"Hello"}""" + "\n\n"
                       + """{"role":"assistant","content":"Hi"}""";

            var messages = client.BuildMessages(prompt);

            Assert.Equal(2, messages.Count);
        }
    }
}
