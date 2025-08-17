
namespace ExternalServices.Clients
{
    using ExternalServices.Contract;
    public interface ILlmClient
    {
        LLMRequest BuildRequest(
            string model,
            List<Message> messages,
            float temperature,
            int maxOutputTokens,
            int topK,
            int topP,
            float frequencyPenalty,
            float presencePenalty
            );

        List<Message> BuildMessages(string prompt);

        LLMResponse GetResponse(LLMRequest request);
    }
}
