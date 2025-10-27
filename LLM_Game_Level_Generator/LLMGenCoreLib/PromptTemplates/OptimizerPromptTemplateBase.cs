namespace LLMPromptProcessor.PromptTemplates
{
    public class OptimizerPromptTemplateBase : IPromptTemplate
    {
        public OptimizerPromptTemplateBase(string filePath)
        {
            this.TemplateFilePath = filePath;
        }

        public string? GameName { get; set; } = "NOT PROVIDED";

        public string? GameDescription { get; set; } = "NOT PROVIDED";

        public string? LevelName { get; set; } = "NOT PROVIDED";

        public required string LevelDescription { get; set; }

        public required string Tiles { get; set; }

        public string? CustomConstraints { get; set; } = string.Empty;

        public string TemplateFilePath { get; set; }
    }
}
