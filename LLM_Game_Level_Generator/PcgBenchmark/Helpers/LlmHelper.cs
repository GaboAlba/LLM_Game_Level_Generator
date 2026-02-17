namespace PcgBenchmark.Helpers
{
    using ExternalServices.Clients;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal static class LlmHelper
    {
        internal static async Task<string> InvokeModelAsync(string prompt)
        {
            var responseMap = string.Empty;
            var apiKeys = GetApiKeys();
            var llmClient = LlmClientFactory.CreateClient(LLMProviders.Providers.OpenAI, LLMProviders.OpenAIClients.Responses, apiKeys.GetValueOrDefault(LLMProviders.Providers.OpenAI.ToString()));

            try
            {
                var messages = llmClient.BuildMessages(prompt);
                var request = llmClient.BuildRequest(messages, false);
                var responsesClient = llmClient as ResponsesClient;
                var response = await llmClient.GetResponseAsync(request, null);

                if (response != null &&
                    response?.Error?.Code == null &&
                    response?.Error?.Message == null &&
                    response?.OutputText != null)
                {
                    responseMap = response.OutputText;
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

        private static Dictionary<string, string> GetApiKeys()
        {
            try
            {
                // TODO: Need to change this to relative path for distribution.
                const string apiKeysDir = "C:\\Users\\Gabriel\\OneDrive\\Documents\\GitHub\\LLM_Game_Level_Generator\\LLM_Game_Level_Generator\\ExternalServices\\api_keys.json";
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
