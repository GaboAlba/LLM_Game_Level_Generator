namespace ExternalServices.Clients
{
    public abstract class LlmClientBase
    {
        protected readonly string model;
        protected readonly string apiKey;
        protected readonly float temperature;
        protected readonly int maxOutputTokens;
        protected readonly int topK;
        protected readonly int topP;
        protected readonly float frequencyPenalty;
        protected readonly float presencePenalty;

        protected abstract ISet<string> AllowedModels { get; }

        public LlmClientBase(
            string model,
            string apiKey,
            float temperature = 0.3f,
            int maxOutputTokens = 10000,
            int topK = 1,
            int topP = 1,
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
