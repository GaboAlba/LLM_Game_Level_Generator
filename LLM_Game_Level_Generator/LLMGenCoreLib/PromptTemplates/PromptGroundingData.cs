namespace LLMGenCoreLib.PromptTemplates
{
    using System.Collections;

    public class PromptGroundingData
    {
        public PromptGroundingData(string filePath)
        {
            this.TemplateFilePath = filePath;
        }

        public string? GameName { get; set; } = "NOT PROVIDED";

        public string? GameDescription { get; set; } = "NOT PROVIDED";

        public string? LevelName { get; set; } = "NOT PROVIDED";

        public required string LevelDescription { get; set; }

        public required string Tiles { get; set; }

        public required string Width { get; set; }

        public required string Height { get; set; }

        public required string GameType { get; set; }

        public string? GameGenre { get; set; }

        public required string DifficultyLevel { get; set; }

        public required string HazardLevel { get; set; }

        public string? CustomConstraints { get; set; } = string.Empty;

        public string TemplateFilePath { get; set; }
    }
}
