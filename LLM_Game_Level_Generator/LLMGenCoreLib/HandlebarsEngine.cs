namespace LLMPromptProcessor
{
    using HandlebarsDotNet;
    using HandlebarsDotNet.IO;

    using LLMPromptProcessor.PromptTemplates;

    using System.IO;
    using System.Text.Json;
    using System.Text.RegularExpressions;

    public class HandlebarsEngine
    {
        private readonly string templateDir;
        private static readonly Regex NewLineRegex = new(@"\r\n", RegexOptions.Compiled);

        public HandlebarsEngine()
        {
            var root = AppContext.BaseDirectory;
            this.templateDir = Path.Combine(root, "Prompts");
        }

        public string ParsePrompt(IPromptTemplate data)
        {
            RegisterHelpers();
            var path = Path.Combine(this.templateDir, data.TemplateFilePath);
            var template = File.ReadAllText(path);
            NewLineRegex.Replace(template, "\n");
            var compiledTemplate = Handlebars.Compile(template);
            var result = compiledTemplate(data);

            return result;
        }

        private static void RegisterHelpers()
        {
            // All handlebar helpers should go here
            Handlebars.RegisterHelper("message", (output, options, context, arguments) =>
            {
                var role = arguments.Length > 0 ? arguments[0] as string : "system";

                // Build an EncodedTextWriter wired to the current configuration
                var formatterProvider = new FormatterProvider(Handlebars.Configuration.FormatterProviders);
                using var buffer = ReusableStringWriter.Get();
                using (var writer = new EncodedTextWriter(buffer, Handlebars.Configuration.TextEncoder, formatterProvider))
                {
                    options.Template(writer, context);   // render the block into our buffer
                }

                var content = buffer.ToString();

                var safeContext = new { role, content };

                output.WriteSafeString(JsonSerializer.Serialize(safeContext));
            });
        }
    }
}
