#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace ExternalServices.Clients.OpenAi
{
    using ExternalServices.Clients;
    using ExternalServices.Contract;
    using ExternalServices.Contract.ReasoningProperty;

    using OpenAI;
    using OpenAI.Responses;

    using System.ClientModel;
    using System.Collections.Generic;
    using System.Text;

    public class ResponsesClient : LlmClientBase, ILlmClient
    {
        /// <summary>
        /// Specific output format. Used for Structured Outputs
        /// </summary>
        public ResponseTextFormat? ResponseTextFormat { get; set; } = null;

        private readonly OpenAIResponseClient responseClient;

        protected override ISet<string> AllowedModels => new HashSet<string>
        {
            // TODO: This must be moved to a YAML or JSON config for better scalability
            "gpt-4.1",
            "gpt-5.2"
        };

        public ResponsesClient(string apiKey)
            : base(
                  model: "gpt-5.2",
                  apiKey: apiKey,
                  temperature: 0.3f,
                  maxOutputTokens: 10000,
                  topK: 0,
                  topP: 0.9f,
                  frequencyPenalty: 0.3f,
                  presencePenalty: 0,
                  clientName: nameof(ResponsesClient))
        {
            var clientOptions = new OpenAIClientOptions
            {
                NetworkTimeout = new TimeSpan(0, 0, seconds: 300)
            };
            var apiKeyCredential = new ApiKeyCredential(apiKey);

            this.responseClient = new OpenAI.Responses.OpenAIResponseClient(this.Model, apiKeyCredential, clientOptions);
        }

        new public List<Message> BuildMessages(string prompt) => base.BuildMessages(prompt);

        public LLMRequest BuildRequest(List<Message> messages, bool shouldStream)
        {
            return new LLMRequest
            {
                Input = messages,
                Model = this.Model,
                MaxOutputTokens = this.MaxOutputTokens,
                Temperature = this.Temperature,
                TopP = this.TopP,
                ReasoningOptions = new ReasoningOptions
                {
                    Effort = Effort.Medium,
                    Summary = Summary.Detailed,
                },
                Stream = shouldStream,
            };
        }

        public async Task<LLMResponse> GetResponseAsync(LLMRequest request, IProgress<string>? reasoningProgress)
        {
            var options = this.SetResponseCreationOptions(request, this.ResponseTextFormat);

            var responseItems = this.CreateOpenAIResponseItemList(request);

            try
            {
                var openAIResponse = await this.responseClient.CreateResponseAsync(responseItems, options);

                var reasoningMessage = openAIResponse.Value.OutputItems.Where(x => x is ReasoningResponseItem).FirstOrDefault() as ReasoningResponseItem;
                if (reasoningMessage != null)
                {
                    reasoningProgress?.Report(reasoningMessage.GetSummaryText());
                }

                return this.GetLLMResponseFromResponsesApiResponse(openAIResponse.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<LLMResponse> GetResponseStreamAsync(LLMRequest request, IProgress<string>? reasoningProgress)
        {
            var options = this.SetResponseCreationOptions(request, this.ResponseTextFormat);

            var responseItems = this.CreateOpenAIResponseItemList(request);

            AsyncCollectionResult<StreamingResponseUpdate> updates = this.responseClient.CreateResponseStreamingAsync(responseItems, options);
            ReasoningResponseItem reasoningResponseItem = new(string.Empty);
            bool reasoningResponseItemAssigned = false;
            var sb = new StringBuilder();
            await foreach (StreamingResponseUpdate update in updates)
            {
                switch (update)
                {
                    case StreamingResponseOutputItemAddedUpdate added
                        when added.Item is ReasoningResponseItem:

                        reasoningResponseItemAssigned = true;
                        sb.AppendLine("[START REASONING]");
                        reasoningProgress?.Report(sb.ToString());
                        break;

                    case StreamingResponseOutputItemDoneUpdate done
                        when done.Item is ReasoningResponseItem reasoningResponse:

                        reasoningResponseItemAssigned = false;
                        sb.Clear();
                        foreach (var part in reasoningResponse.SummaryParts)
                        {
                            if (part is ReasoningSummaryTextPart text)
                            {
                                sb.Append(text.Text);
                                reasoningProgress?.Report(sb.ToString());
                            }
                        }

                        break;

                    case StreamingResponseOutputTextDeltaUpdate delta
                         when reasoningResponseItemAssigned:

                        sb.Append(delta.Delta);
                        reasoningProgress?.Report(sb.ToString());

                        break;

                    case StreamingResponseCompletedUpdate completed:
                        return this.GetLLMResponseFromResponsesApiResponse(completed.Response);
                }
            }

            return new LLMResponse { Id = "" };
        }

        private ResponseCreationOptions SetResponseCreationOptions(LLMRequest request, ResponseTextFormat? responseTextFormat = null)
        {
            var responseCreationOptions = new ResponseCreationOptions
            {
                MaxOutputTokenCount = request.MaxOutputTokens,
                TruncationMode = request.Truncation,
                Temperature = request.Temperature,
                TopP = request.TopP,
                TextOptions = new ResponseTextOptions
                {
                    TextFormat = responseTextFormat,
                },
                ServiceTier = ResponseServiceTier.Flex, // This wnsures lower costs (equivalent to the Batch API)
            };

            if (this.ReasoningModels.Contains(request.Model) && request.ReasoningOptions != null)
            {
                responseCreationOptions.Temperature = null; // WORKAROUND: Needs to be set to null for reasoning models
                responseCreationOptions.TopP = null; // WORKAROUND: Needs to be set to null for reasoning models
                responseCreationOptions.ReasoningOptions = new()
                {
                    ReasoningEffortLevel = request.ReasoningOptions.GetEffort(),
                    ReasoningSummaryVerbosity = request.ReasoningOptions.GetSummary(),
                };
            }

            // Reset Text Format
            this.ResponseTextFormat = null;

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
