namespace LLM_Game_Level_Generator.Clients.GoogleAI
{
    public class EmbeddingsClient : GoogleAIClient
    {
        private readonly string AvailableModels;

        public EmbeddingsClient(string apiKey)
            : base(apiKey) { }
    }
}
