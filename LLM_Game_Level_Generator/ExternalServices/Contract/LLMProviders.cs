namespace ExternalServices.Contract
{
    public static class LLMProviders
    {
        public enum Providers
        {
            GoogleAI,
            OpenAI
        }

        public enum GoogleAIClients
        {
            Embeddings = 1,
            Other = 2,
        }

        public enum OpenAIClients
        {
            Embeddings,
            ChatCompletions,
            Completions,
            Responses
        }

        public static bool IsSupportedClientType(Enum clientType)
        {
            if (clientType is GoogleAIClients ||
                clientType is OpenAIClients)
            {
                return true;
            }

            return false;
        }
    }
}
