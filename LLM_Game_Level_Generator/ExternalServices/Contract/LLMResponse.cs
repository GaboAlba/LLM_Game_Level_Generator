namespace ExternalServices.Contract
{
    using ExternalServices.Contract.LLM_Response;

    public class LLMResponse
    {
        /// <summary>
        /// Unique identifier for this response. Useful for debugging in the future
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Model Id used to generate the response
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// An error object returned when the model fails to generate a response
        /// </summary>
        public LLMError? Error { get; set; }

        /// <summary>
        /// An upper bound for the number of tokens that can be generated for a response
        /// </summary>
        public int? MaxOutputTokens { get; set; }

        /// <summary>
        /// Aggregated text output from 
        /// </summary>
        public string? OutputText { get; set; }

        /// <summary>
        /// The reasoning performed by the LLM to achieve the response
        /// </summary>
        public string? ReasoningText { get; set; }

        /// <summary>
        /// Details on token usage for the request
        /// </summary>
        public LLMUsage? Usage { get; set; }
    }
}
