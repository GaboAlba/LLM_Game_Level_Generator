namespace LLM_Game_Level_Generator.Clients
{
    using LLM_Game_Level_Generator.Models;
    using LLM_Game_Level_Generator.Clients.GoogleAI;
    using LLM_Game_Level_Generator.OpenAi;
    using OpenAI;
    using OpenAI.Responses;

    public class LlmClientFactory
    {
        public static object CreateClient(LLMProviders.Providers llmProvider, Enum clientType, string apiKey, string model)
        {
            if (!LLMProviders.IsSupportedClientType(clientType))
            {
                throw new NotSupportedException($"The client type {clientType.GetType()} is not supported");
            }

            switch(llmProvider)
            {
                case LLMProviders.Providers.GoogleAI:
                {
                    switch(clientType)
                    {
                        case LLMProviders.GoogleAIClients.Embeddings:
                            return new EmbeddingsClient(apiKey);
                        case LLMProviders.GoogleAIClients.Other:
                        default:
                                return new GoogleAIClient(apiKey);
                    }
                }
                case LLMProviders.Providers.OpenAI:
                default:
                {
                    switch(clientType)
                    {
                        case LLMProviders.OpenAIClients.Embeddings:
                            throw new NotImplementedException();
                        case LLMProviders.OpenAIClients.ChatCompletions:
                            throw new NotImplementedException();
                        case LLMProviders.OpenAIClients.Completions:
                            throw new NotImplementedException();
                        case LLMProviders.OpenAIClients.Responses:
                        default:
                            return new ResponsesClient(model, apiKey);
                    }

                }
            }
        }

    }
}
