namespace ExternalServices.Contract.ReasoningProperty
{
    public class ReasoningOptions
    {
        public Effort Effort { get; set; }

        public Summary Summary { get; set; }

        public string GetEffort() => this.Effort.ToString().ToLower();

        public string? GetSummary() => this.Summary switch
        {
            Summary.None => null,
            _ => this.Summary.ToString().ToLower(),
        };
    }
}
