namespace LLM_Game_Level_Generator.RAG.Contract
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    public sealed class GameLevelContract
    {
        public List<float> Embedding { get; set; } = new();

        public string FilePath { get; set; } = string.Empty;

        public string Prompt { get; set; } = string.Empty;

        public string Output { get; set; } = string.Empty;

        public string Content => this.Prompt + "\n" + this.Output;

        public async Task ReadContentAsync()
        {
            var fullText = await File.ReadAllBytesAsync(this.FilePath);
            var stream = new MemoryStream(fullText);
            var jsonObject = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream);
            this.Prompt = jsonObject?.GetValueOrDefault("Prompt") ?? throw new JsonException($"Prompt is null or invalid for {this.FilePath}");
            this.Output = jsonObject?.GetValueOrDefault("Output") ?? throw new JsonException($"Output is null or invalid for {this.FilePath}");
        }
    }
}
