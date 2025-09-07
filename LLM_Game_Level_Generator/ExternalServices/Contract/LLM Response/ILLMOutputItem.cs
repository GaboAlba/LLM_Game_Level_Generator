namespace ExternalServices.Contract.LLM_Response
{
    using ExternalServices.Contract.LLM_Response.OutputItems;

    using System.Text.Json.Serialization;

    [JsonDerivedType(typeof(OutputMessage))]
    public interface ILLMOutputItem
    {
        string Type { get; }

        string Id { get; set; }
    }
}
