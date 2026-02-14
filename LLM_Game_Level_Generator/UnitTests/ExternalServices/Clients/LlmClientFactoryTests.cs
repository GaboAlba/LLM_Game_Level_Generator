namespace UnitTests
{
    using ExternalServices.Clients;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    public class LlmClientFactoryTests
    {
        private const string TestApiKey = "test-api-key-for-factory";

        [Fact]
        public void CreateClient_WithOpenAIResponses_ReturnsResponsesClient()
        {
            var client = LlmClientFactory.CreateClient(
                LLMProviders.Providers.OpenAI,
                LLMProviders.OpenAIClients.Responses,
                TestApiKey);

            Assert.IsType<ResponsesClient>(client);
        }

        [Fact]
        public void CreateClient_WithOpenAIEmbeddings_ThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
                LlmClientFactory.CreateClient(
                    LLMProviders.Providers.OpenAI,
                    LLMProviders.OpenAIClients.Embeddings,
                    TestApiKey));
        }

        [Fact]
        public void CreateClient_WithOpenAIChatCompletions_ThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
                LlmClientFactory.CreateClient(
                    LLMProviders.Providers.OpenAI,
                    LLMProviders.OpenAIClients.ChatCompletions,
                    TestApiKey));
        }

        [Fact]
        public void CreateClient_WithOpenAICompletions_ThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
                LlmClientFactory.CreateClient(
                    LLMProviders.Providers.OpenAI,
                    LLMProviders.OpenAIClients.Completions,
                    TestApiKey));
        }

        [Fact]
        public void CreateClient_WithGoogleAI_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() =>
                LlmClientFactory.CreateClient(
                    LLMProviders.Providers.GoogleAI,
                    LLMProviders.GoogleAIClients.None,
                    TestApiKey));
        }

        [Fact]
        public void CreateClient_WithUnsupportedClientType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() =>
                LlmClientFactory.CreateClient(
                    LLMProviders.Providers.OpenAI,
                    DayOfWeek.Monday,
                    TestApiKey));
        }
    }
}
