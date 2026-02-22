namespace PcgBenchmark
{
    using PcgBenchmark.Helpers;

    using System;

    internal partial class Program
    {
        internal async static Task Run(string[] args)
        {
            var output = await ConsoleHelper.HandleRequestAsync(args);
            if (output.Value.Error != null && output.Value.Error != string.Empty)
            {
                Console.WriteLine($"ERROR: {output.Value.Error}");
                return;
            }
            else if (output.Value.DebugMessage != null && output.Value.DebugMessage != string.Empty)
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
