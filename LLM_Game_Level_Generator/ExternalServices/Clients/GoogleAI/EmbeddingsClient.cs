namespace ExternalServices.Clients.GoogleAI
{
    using ExternalServices.Contract;

    public class EmbeddingsClient : LlmClientBase, ILlmClient
    {
        protected override ISet<string> AllowedModels => new HashSet<string>
        {
            "SampleModel",
        };

        public EmbeddingsClient(string apiKey)
            : base(
                  model:"modelString",
                  apiKey:apiKey,
                  clientName: nameof(EmbeddingsClient)) { }

        public List<double> GetEmbeddingVector(string text)
        {
            // TODO: Replace to actual implementation
            return new List<double>(text.Count());
        }

        public LLMRequest BuildRequest(List<Message> messages) => throw new NotImplementedException();
        public List<Message> BuildMessages(string prompt) => throw new NotImplementedException();
        public LLMResponse GetResponse(LLMRequest request) => throw new NotImplementedException();
    }
}
