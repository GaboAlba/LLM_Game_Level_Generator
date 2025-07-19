
namespace LLM_Game_Level_Generator.Clients
{
    using LLM_Game_Level_Generator.Models;
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
