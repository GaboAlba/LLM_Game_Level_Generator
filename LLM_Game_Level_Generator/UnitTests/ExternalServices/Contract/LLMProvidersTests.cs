namespace UnitTests
{
    using ExternalServices.Contract;

    public class LLMProvidersTests
    {
        [Theory]
        [InlineData(LLMProviders.OpenAIClients.Embeddings)]
        [InlineData(LLMProviders.OpenAIClients.ChatCompletions)]
        [InlineData(LLMProviders.OpenAIClients.Completions)]
        [InlineData(LLMProviders.OpenAIClients.Responses)]
        public void IsSupportedClientType_WithOpenAIClientType_ReturnsTrue(LLMProviders.OpenAIClients clientType)
        {
            var result = LLMProviders.IsSupportedClientType(clientType);

            Assert.True(result);
        }

        [Fact]
        public void IsSupportedClientType_WithGoogleAIClientType_ReturnsTrue()
        {
            var result = LLMProviders.IsSupportedClientType(LLMProviders.GoogleAIClients.None);

            Assert.True(result);
        }

        [Fact]
        public void IsSupportedClientType_WithUnsupportedEnumType_ReturnsFalse()
        {
            var unsupportedEnum = DayOfWeek.Monday;

            var result = LLMProviders.IsSupportedClientType(unsupportedEnum);

            Assert.False(result);
        }

        [Fact]
        public void ProvidersEnum_ContainsExpectedValues()
        {
            var values = Enum.GetValues<LLMProviders.Providers>();

            Assert.Contains(LLMProviders.Providers.GoogleAI, values);
            Assert.Contains(LLMProviders.Providers.OpenAI, values);
            Assert.Equal(2, values.Length);
        }

        [Fact]
        public void OpenAIClientsEnum_ContainsExpectedValues()
        {
            var values = Enum.GetValues<LLMProviders.OpenAIClients>();

            Assert.Contains(LLMProviders.OpenAIClients.Embeddings, values);
            Assert.Contains(LLMProviders.OpenAIClients.ChatCompletions, values);
            Assert.Contains(LLMProviders.OpenAIClients.Completions, values);
            Assert.Contains(LLMProviders.OpenAIClients.Responses, values);
            Assert.Equal(4, values.Length);
        }
    }
}
