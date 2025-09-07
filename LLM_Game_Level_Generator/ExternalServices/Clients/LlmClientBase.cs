namespace ExternalServices.Clients
{
    public abstract class LlmClientBase
    {
        // LLM Hyperparamters
        protected readonly string model;
        protected readonly string apiKey;
        protected readonly float temperature;
        protected readonly int maxOutputTokens;
        protected readonly float topK;
        protected readonly float topP;
        protected readonly float frequencyPenalty;
        protected readonly float presencePenalty;

        protected abstract ISet<string> AllowedModels { get; }
        protected HashSet<string> ReasoningModels = new()
        {
            "gpt-5",
            "gpt-o1",
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

            this.model = model;
            this.apiKey = apiKey;
            this.temperature = temperature;
            this.maxOutputTokens = maxOutputTokens;
            this.topK = topK;
            this.topP = topP;
            this.frequencyPenalty = frequencyPenalty;
            this.presencePenalty = presencePenalty;
        }
    }
}
