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

        public ResponsesClient(string apiKey)
            : base(
                  model: "gpt-4.1",
                  apiKey: apiKey,
                  temperature: 0.3f,
                  maxOutputTokens: 10000,
                  topK: 1,
                  topP: 1,
                  frequencyPenalty: 0,
                  presencePenalty: 0,
                  clientName:nameof(ResponsesClient)) { }

        public List<Message> BuildMessages(string prompt)
        {
            throw new NotImplementedException();
        }

        public LLMRequest BuildRequest(List<Message> messages)
        {
            throw new NotImplementedException();
        }

        public LLMResponse GetResponse(LLMRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
