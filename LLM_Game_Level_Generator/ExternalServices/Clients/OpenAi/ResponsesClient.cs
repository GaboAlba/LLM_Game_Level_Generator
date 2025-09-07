#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace ExternalServices.Clients.OpenAi
{
    using ExternalServices.Clients;
    using ExternalServices.Contract;
    using ExternalServices.Contract.ReasoningProperty;

    using OpenAI.Responses;

    using System.Collections.Generic;

    public class ResponsesClient : LlmClientBase, ILlmClient
    {
        private readonly OpenAIResponseClient responseClient;

        protected override ISet<string> AllowedModels => new HashSet<string>
        {
            // TODO: This must be moved to a YAML or JSON config for better scalability
            "gpt-4.1"
        };

        public ResponsesClient(string apiKey)
            : base(
                  model: "gpt-4.1",
                  apiKey: apiKey,
                  temperature: 0.3f,
                  maxOutputTokens: 10000,
                  topK: 1,
                  topP: 1,
                  frequencyPenalty: 0,
                  presencePenalty: 0,
                  clientName:nameof(ResponsesClient))
        {
            this.responseClient = new OpenAI.Responses.OpenAIResponseClient(this.model, this.apiKey);
        }

        public List<Message> BuildMessages(string prompt)
        {
            // $"BuildMessages is currently only a sample of the actual functionality which will be implemented alongside the handlebar compiler"
            var sampleMessagesList = new List<Message>
            {
                new Message
                {
                    Role = "User",
                    Content = "Guess a number between 1 and 10",
                },
            };
            return sampleMessagesList;
        }

        public LLMRequest BuildRequest(List<Message> messages)
        {
            return new LLMRequest
            {
                Input = messages,
                Model = this.model,
                MaxOutputTokens = this.maxOutputTokens,
                Temperature = this.temperature,
                TopP = this.topP,
            };
        }

        public async Task<LLMResponse> GetResponseAsync(LLMRequest request)
        {
            var options = this.SetResponseCreationOptions(request);

            var responseItems = this.CreateOpenAIResponseItemList(request);

            var openAIResponse = await this.responseClient.CreateResponseAsync(responseItems, options);
            return this.GetLLMResponseFromResponsesApiResponse(openAIResponse.Value);
        }

        private ResponseCreationOptions SetResponseCreationOptions(LLMRequest request)
        {
            var responseCreationOptions =  new ResponseCreationOptions
            {
                MaxOutputTokenCount = request.MaxOutputTokens,
                TruncationMode = request.Truncation,
                Temperature = request.Temperature,
                TopP = request.TopP,
            };

            if (this.ReasoningModels.Contains(request.Model) && request.ReasoningOptions != null)
            {
                responseCreationOptions.ReasoningOptions = new()
                {
                    ReasoningEffortLevel = request.ReasoningOptions.GetEffort(),
                    ReasoningSummaryVerbosity = request.ReasoningOptions.GetSummary(),
                };
            }

            return responseCreationOptions;
        }

        private List<ResponseItem> CreateOpenAIResponseItemList(LLMRequest request)
        {
            var responseItems = new List<ResponseItem>();
            foreach (var message in request.Input)
            {
                ResponseItem itemToAdd;
                switch (message.Role)
                {
                    case "user":
                        itemToAdd = ResponseItem.CreateUserMessageItem(message.Content);
                        break;
                    case "assistant":
                        itemToAdd = ResponseItem.CreateAssistantMessageItem(message.Content);
                        break;
                    case "system":
                    default:
                        itemToAdd = ResponseItem.CreateSystemMessageItem(message.Content);
                        break;
                }

                // Add corresponding item
                responseItems.Add(itemToAdd);
            }

            return responseItems;
        }

        private LLMResponse GetLLMResponseFromResponsesApiResponse(OpenAIResponse openAIResponse)
        {
            return new LLMResponse
            {
                Id = openAIResponse.Id,
                Model = openAIResponse.Model,
                Error = new Contract.LLM_Response.LLMError
                {
                    Code = openAIResponse.Error?.Code.ToString(),
                    Message = openAIResponse.Error?.Message,
                },
                MaxOutputTokens = openAIResponse.MaxOutputTokenCount,
                OutputText = openAIResponse.GetOutputText(),
            };
        }
    }
}
#pragma warning restore OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
