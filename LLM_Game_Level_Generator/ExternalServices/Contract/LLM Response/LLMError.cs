namespace ExternalServices.Contract.LLM_Response
{
    public class LLMError
    {

        /// <summary>
        /// The error code for the response
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Human-readable description of the error
        /// </summary>
        public string? Message { get; set; }
    }
}
