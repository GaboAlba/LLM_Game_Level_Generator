namespace ExternalServices.Clients.OpenAi
{
    using ExternalServices.Clients;
    using ExternalServices.Contract;
    using System.Collections.Generic;
    public class ResponsesClient : LlmClientBase, ILlmClient
    {
        protected override ISet<string> AllowedModels => new HashSet<string>
        {
            // TODO: This must be moved to a YAML or JSON config for better scalability
            "TestModel",
            "TestModel1"
        };

        public ResponsesClient(string model, string apiKey)
            : base(model, apiKey, nameof(ResponsesClient)) { }

        public List<Message> BuildMessages(string prompt)
        {
            throw new NotImplementedException();
        }

        public LLMRequest BuildRequest(string model, List<Message> messages, float temperature, int maxOutputTokens, int topK, int topP, float frequencyPenalty, float presencePenalty)
        {
            throw new NotImplementedException();
        }

        public LLMResponse GetResponse(LLMRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
