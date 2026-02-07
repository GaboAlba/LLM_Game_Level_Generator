
namespace ExternalServices.Clients
{
    using ExternalServices.Contract;
    public interface ILlmClient
    {
        LLMRequest BuildRequest(List<Message> messages, bool shouldStream = false);

        List<Message> BuildMessages(string prompt);

        Task<LLMResponse> GetResponseAsync(LLMRequest request, IProgress<string>? reasoningProgress);

        Task<LLMResponse> GetResponseStreamAsync(LLMRequest request, IProgress<string>? reasoningProgress);
    }
}
