#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace PcgBenchmark.Helpers
{
    using ExternalServices.Clients;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    using GeneratorViewModel;

    using LLMGenCoreLib.PromptTemplates;

    using LLMPromptProcessor.PromptTemplates;

    using OpenAI.Responses;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal static class LlmHelper
    {
        internal static async Task<string> InvokeModelAsync(string prompt, PromptTemplateV1 benchmarkObject, string model = "gpt-5.2")
        {
            var responseMap = string.Empty;
            var apiKeys = GetApiKeys();
            var llmClient = LlmClientFactory.CreateClient(LLMProviders.Providers.OpenAI, LLMProviders.OpenAIClients.Responses, apiKeys.GetValueOrDefault(LLMProviders.Providers.OpenAI.ToString()));

            try
            {
                var messages = llmClient.BuildMessages(prompt);
                var request = llmClient.BuildRequest(messages, false);
                var responsesClient = llmClient as ResponsesClient;
                responsesClient?.ResponseTextFormat = ResponseTextFormat.CreateJsonSchemaFormat(
                   jsonSchemaFormatName: "2d-map-grid-format",
                   jsonSchema: BinaryData.FromString(PromptUserData.GetMapResponseJsonSchema(
                       height: int.Parse(benchmarkObject.Height),
                       width: int.Parse(benchmarkObject.Width),
                       mapTiles: PromptGroundingDataInjector.StringToList(benchmarkObject.Tiles) as ObservableCollection<MapTile>)),
                   jsonSchemaIsStrict: true);

                if (GetValidModels().Contains(model))
                {
                    responsesClient.Model = model;
                }

                var response = await llmClient.GetResponseAsync(request, null) ?? null;

                if (response != null &&
                    response?.Error?.Code == null &&
                    response?.Error?.Message == null &&
                    response?.OutputText != null)
                {
                    var responseObject = JsonSerializer.Deserialize<Map>(response.OutputText);
                    string responseLines = string.Empty;
                    foreach (var line in responseObject.MapGrid.EnumerateLines())
                    {
                        responseLines += line.ToString() + '\n';
                    }

                    // Eliminate trailing whitespace at the end
                    responseLines = responseLines.Trim();

                    responseMap = responseLines;
                    return responseMap;
                }
                else
                {
                    throw new NullReferenceException(
                        $"Either the LLM response, or the output are null. Or there was an error" +
                        $"Error: {response?.Error}, " +
                        $"Output: {response?.OutputText}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        internal static ISet<string> GetValidModels()
        {
            return new HashSet<string>
            {
                "gpt-4.1",
                "gpt-5.2"
            };
        }

        internal static bool IsValidModel(string model, out ISet<string> validModels)
        {
            validModels = GetValidModels();
            return validModels.Contains(model);
        }

        private static Dictionary<string, string> GetApiKeys()
        {
            try
            {
                // TODO: Need to change this to relative path for distribution.
                const string apiKeysDir = "api_keys.json";
                if (!File.Exists(apiKeysDir))
                {
                    var options = new EnumerationOptions
                    {
                        RecurseSubdirectories = true,
                        IgnoreInaccessible = true,
                        AttributesToSkip = FileAttributes.Hidden | FileAttributes.System | FileAttributes.Temporary | FileAttributes.Compressed | FileAttributes.Encrypted,
                    };

                    var allFiles = Directory.GetFiles("..\\..\\..\\..\\", apiKeysDir, options);
                    
                    var file = File.ReadAllText(allFiles[0]);
                    return JsonSerializer.Deserialize<Dictionary<string, string>>(file);

                }

                var jsonString = File.ReadAllText(apiKeysDir);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: API Keys deserializing failed. Check the existence and/or format of the json file. Exception: {ex}");
                throw;
            }
        }
    }
}
