namespace ExternalServices.Clients
{
    using ExternalServices.Clients.GoogleAI;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    public class LlmClientFactory
    {
        public static ILlmClient CreateClient(LLMProviders.Providers llmProvider, Enum clientType, string apiKey)
        {
            if (!LLMProviders.IsSupportedClientType(clientType))
            {
                throw new NotSupportedException($"The client type {clientType.GetType()} is not supported");
            }

            switch (llmProvider)
            {
                case LLMProviders.Providers.GoogleAI:
                    {
                        switch (clientType)
                        {
                            case LLMProviders.GoogleAIClients.Embeddings:
                            default:
                                return new EmbeddingsClient(apiKey);
                        }
                    }
                case LLMProviders.Providers.OpenAI:
                default:
                    {
                        switch (clientType)
                        {
                            case LLMProviders.OpenAIClients.Embeddings:
                                throw new NotImplementedException();
                            case LLMProviders.OpenAIClients.ChatCompletions:
                                throw new NotImplementedException();
                            case LLMProviders.OpenAIClients.Completions:
                                throw new NotImplementedException();
                            case LLMProviders.OpenAIClients.Responses:
                            default:
                                return new ResponsesClient(apiKey);
                        }

                    }
            }
        }

    }
}
