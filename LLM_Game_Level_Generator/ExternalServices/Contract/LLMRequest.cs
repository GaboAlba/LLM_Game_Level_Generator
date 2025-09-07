namespace ExternalServices.Contract
{
    using ExternalServices.Contract.ReasoningProperty;

    using System;
    using System.Collections.Generic;

    public class LLMRequest
    {
        public required List<Message> Input { get; set; }

        public required string Model { get; set; }

        public required int MaxOutputTokens { get; set; } = 1000;

        public int? MaxToolCalls { get; set; }

        public ReasoningOptions? ReasoningOptions { get; set; }

        public string? Truncation { get; set; } = "auto";

        public string? StopSequences { get; set; }

        public required float Temperature
        {
            get;
            set
            {
                if (value < 0 || value > 2)
                {
                    throw new ArgumentException("Temperature must be a value in between 0 and 2");
                }

                field = value;
            }
        } = 0.5f;

        public float TopP
        {
            get;
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("TopP must be a value between 0 and 1");
                }

                field = value;
            }
        } = 1f;

        public float? TopK
        {
            get;
            set
            {
                if (value < 1 || value > 10) // This can be higher, but can create very non-deterministic results.
                {
                    throw new ArgumentException("TopK must be a value between 0 and 100");
                }

                field = value;
            }
        }
    }
}
