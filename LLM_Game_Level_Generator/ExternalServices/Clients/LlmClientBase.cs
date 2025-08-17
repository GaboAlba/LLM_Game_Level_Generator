namespace ExternalServices.Clients
{
    public abstract class LlmClientBase
    {
        protected readonly string model;

        protected readonly string apiKey;

        protected abstract ISet<string> AllowedModels { get; }

        public LlmClientBase(string model, string apiKey, string clientName)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(apiKey);

            if (!this.AllowedModels.Contains(model))
            {
                throw new ArgumentException($"Invalid {clientName} model selected: {model}");
            }

            this.model = model;
            this.apiKey = apiKey;
        }
    }
}
