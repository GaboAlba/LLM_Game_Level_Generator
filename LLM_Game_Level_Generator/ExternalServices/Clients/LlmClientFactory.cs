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
                            default:
                                throw new NotSupportedException("Google AI LLM Models are not yet supported");
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
