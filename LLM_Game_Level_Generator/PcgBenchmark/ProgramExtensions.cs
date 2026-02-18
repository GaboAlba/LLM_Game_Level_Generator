namespace PcgBenchmark
{
    using PcgBenchmark.Helpers;

    using System;

    internal partial class Program
    {
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
