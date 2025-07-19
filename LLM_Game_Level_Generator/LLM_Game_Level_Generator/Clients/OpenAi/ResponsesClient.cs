namespace LLM_Game_Level_Generator.OpenAi
{
    using LLM_Game_Level_Generator.Clients;
    using LLM_Game_Level_Generator.Models;
    using OpenAI.Responses;
    using System.Collections.Generic;
#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    public class ResponsesClient : OpenAIResponseClient, ILlmClient
#pragma warning restore OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    {
        public ResponsesClient(string model, string apiKey) 
        : base(model, apiKey)
        { }

        public List<Message> BuildMessages(string prompt)
        {
            throw new NotImplementedException();
        }

        public LLMRequest BuildRequest(string model, List<Message> messages, float temperature, int maxOutputTokens, int topK, int topP, float frequencyPenalty, float presencePenalty)
        {
            throw new NotImplementedException();
        }

        public LLMResponse GetResponse(LLMRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
