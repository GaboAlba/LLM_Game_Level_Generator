namespace PcgBenchmark
{
    using LLMPromptProcessor.PromptTemplates;

    using PcgBenchmark.Helpers;

    using System;
    using System.Collections.Generic;

    internal partial class Program
    {
        private readonly Dictionary<string, PromptTemplateV1>? benchmarksToRun;
        internal async static void Run(string[] args)
        {
            var output = await ConsoleHelper.HandleRequestAsync(args);
            if (output.Value.Error != null)
            {
                Console.WriteLine($"ERROR: {output.Value.Error}");
                return;
            }
            else if (output.Value.DebugMessage != null)
            {
                Console.WriteLine(output.Value.DebugMessage);
                return;
            }
            else
            {
                Console.WriteLine("SUCCESS!");
            }
        }
    }
}
