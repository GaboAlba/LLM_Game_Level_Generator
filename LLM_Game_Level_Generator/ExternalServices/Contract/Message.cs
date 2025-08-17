namespace ExternalServices.Contract
{
    /// <summary>
    /// Message object for the LLM
    /// </summary>
    public record Message
    {
        /// <summary>
        /// The role of the LLM Message
        /// </summary>
        public required string Role
        {
            get;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Role is null");
                }

                if (value != "system" ||
                    value != "user" ||
                    value != "assistant")
                {
                    throw new ArgumentException($"Role {value} is not valid. It must either system, user, or assistant");
                }

                field = value;
            }
        }

        /// <summary>
        /// The content of the Prompt
        /// </summary>
        public required string Content { get; set; }
    }
}
