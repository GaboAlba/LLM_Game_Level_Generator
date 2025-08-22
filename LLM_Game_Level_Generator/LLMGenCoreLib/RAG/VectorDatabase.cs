namespace LLM_Game_Level_Generator.RAG
{
    using LLM_Game_Level_Generator.RAG.Contract;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    public class VectorDatabase
    {
        public List<Dictionary<Guid, GameLevelContract>> VectorList { get; set; } = new();

        public string DatabaseRootPath { get; set; } = string.Empty;

        public string DatabaseFileName { get; set; } = "GameLevelVectorDatabase.json";

        public string GroundingFilesPath { get; set; } = string.Empty;

        private string Database => Path.Join(this.DatabaseRootPath, this.DatabaseFileName);

        public async void FetchAsync()
        {
            var fileExists = this.FileAndDirectoryExist();
            if (fileExists)
            {
                var jsonText = await File.ReadAllBytesAsync(this.Database);
                var stream = new MemoryStream(jsonText);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                this.VectorList.Clear();
                this.VectorList = await JsonSerializer.DeserializeAsync<List<Dictionary<Guid, GameLevelContract>>>(stream, options) ?? throw new JsonException($"Deserialization was not possible for {this.Database}");
            }
            else
            {

            }
        }

        private async void CreateVectorList()
        {
            foreach (var file in Directory.EnumerateFiles(this.GroundingFilesPath))
            {
                if (!file.EndsWith(".json"))
                {
                    // TODO: Add logging
                    continue;
                }

                var document = new GameLevelContract
                {
                    Embedding = [],
                    FilePath = file,
                };
                await document.ReadContentAsync();
            }
        }

        private bool FileAndDirectoryExist()
        {
            if (!Path.Exists(this.DatabaseRootPath))
            {
                Directory.CreateDirectory(this.DatabaseRootPath);
                File.Create(this.DatabaseFileName);
                return false;
            }
            else if (!File.Exists(this.DatabaseFileName))
            {
                File.Create(this.DatabaseFileName);
                return false;
            }

            return true;
        }
    }
}
