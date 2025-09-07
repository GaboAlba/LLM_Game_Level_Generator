
namespace ExternalServices.Clients
{
    using ExternalServices.Contract;
    public interface ILlmClient
    {
        LLMRequest BuildRequest(List<Message> messages);

        List<Message> BuildMessages(string prompt);

        Task<LLMResponse> GetResponseAsync(LLMRequest request);
    }
}
