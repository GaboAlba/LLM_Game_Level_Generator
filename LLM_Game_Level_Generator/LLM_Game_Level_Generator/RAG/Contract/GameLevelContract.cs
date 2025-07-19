namespace LLM_Game_Level_Generator.RAG.Contract
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public sealed class GameLevelContract
    {
        public List<float> Embedding {  get; set; } = new ();

        public string FilePath { get; set; } = string.Empty;

        public string Prompt { get; set; } = string.Empty;

        public string Output { get; set; } = string.Empty;

        public string Content => this.Prompt + "\n" + this.Output;

        public async void ReadContent()
        {
            var fullText = await File.ReadAllBytesAsync(this.FilePath);
            var stream = new MemoryStream(fullText);
            var jsonObject = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream);
            this.Prompt = jsonObject.GetValueOrDefault("Prompt");
            this.Output = jsonObject.GetValueOrDefault("Output");
        }
    }
}
