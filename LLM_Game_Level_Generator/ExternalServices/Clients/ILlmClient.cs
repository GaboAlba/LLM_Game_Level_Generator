
namespace ExternalServices.Clients
{
    using ExternalServices.Contract;
    public interface ILlmClient
    {
        LLMRequest BuildRequest(List<Message> messages);

        List<Message> BuildMessages(string prompt);

        LLMResponse GetResponse(LLMRequest request);
    }
}
