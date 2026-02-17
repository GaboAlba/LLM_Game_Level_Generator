namespace PcgBenchmark
{
    using LLMPromptProcessor.PromptTemplates;

    using PcgBenchmark.Helpers;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal partial class Program
    {
        private readonly Dictionary<string, PromptTemplateV1>? benchmarksToRun;
        internal async static void Run(string[] args)
        {
            string prompt = string.Empty;
            string map = string.Empty;

            map = await LlmHelper.InvokeModelAsync(prompt);
            Console.WriteLine($"The generated map is:");
            Console.WriteLine($"{map}");
            return;
        }
    }
}
