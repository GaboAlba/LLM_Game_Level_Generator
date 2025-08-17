namespace ExternalServices.Clients.GoogleAI
{
    public class EmbeddingsClient : LlmClientBase
    {
        protected override ISet<string> AllowedModels => new HashSet<string>
        {
            "SampleModel",
        };

        public EmbeddingsClient(string model, string apiKey)
            : base(model, apiKey, nameof(EmbeddingsClient)) { }

        public List<double> GetEmbeddingVector(string text)
        {
            // TODO: Replace to actual implementation
            return new List<double>(text.Count());
        }

        private readonly string url = "";
    }
}
