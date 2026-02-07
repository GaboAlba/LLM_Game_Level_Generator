namespace ExternalServices.Clients
{
    using ExternalServices.Contract;

    using System.Text.Json;

    public abstract class LlmClientBase
    {
        // LLM Hyperparamters
        public string Model { get; set; }
        public string ApiKey { get; set; }
        public float Temperature { get; set; }
        public int MaxOutputTokens { get; set; }
        public float TopK { get; set; }
        public float TopP { get; set; }
        public float FrequencyPenalty { get; set; }
        public float PresencePenalty { get; set; }

        protected abstract ISet<string> AllowedModels { get; }
        protected HashSet<string> ReasoningModels = new()
        {
            "gpt-5",
            "gpt-o1",
            "gpt-5.2"
        };

        public LlmClientBase(
            string model,
            string apiKey,
            float temperature = 0.3f,
            int maxOutputTokens = 10000,
            float topK = 1,
            float topP = 1,
            float frequencyPenalty = 0,
            float presencePenalty = 0,
            string? clientName = null)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(apiKey);

            if (!this.AllowedModels.Contains(model))
            {
                throw new ArgumentException($"Invalid {clientName} model selected: {model}");
            }

            this.Model = model;
            this.ApiKey = apiKey;
            this.Temperature = temperature;
            this.MaxOutputTokens = maxOutputTokens;
            this.TopK = topK;
            this.TopP = topP;
            this.FrequencyPenalty = frequencyPenalty;
            this.PresencePenalty = presencePenalty;
        }

        public List<Message> BuildMessages(string prompt)
        {
            var lines = prompt.Split(new[] { "\n", "\r\n" }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var messages = new List<Message>();
            foreach (var line in lines)
            {
                messages.Add(JsonSerializer.Deserialize<Message>(line));
            }

            return messages;
        }
    }
}
