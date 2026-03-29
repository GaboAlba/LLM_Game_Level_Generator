namespace GeneratorUI
{
    using ExternalServices.Clients;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    using GeneratorViewModel;

    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text.Json;

    public partial class MainWindow
    {
        public ObservableCollection<MapTile> MapTileOptions { get; set; }
        public GeneralElements GeneralElements { get; set; }
        public MapConstraints MapConstraints { get; set; }
        public Output Output { get; set; }
        public Model Model { get; set; }
        public Array GameTypeArray { get; set; }
        public Array DifficultyArray { get; set; }
        public Array HazardLevelArray { get; set; }
        public FontProperties FontProperties { get; set; }
        public Array AvailableModels { get; set; }

        // ApiKeysUI section
        public Array LlmProvidersArray { get; set; }
        public ObservableCollection<GeneratorViewModel.LlmApiKey> llmApiKeys { get; set; }

        private HashSet<string> usedCharacters = new HashSet<string>();
        private ILlmClient LlmClient;
        private Dictionary<string, string> apiKeys;
        private PromptUserData promptUserData;
        private string saveFilePath;
        private bool savedInCurrentSession = false;
        private bool modifiedInCurrentSession = false;
        

        // TODO: Need to change this to relative path for distribution.
        private const string ApiKeyFileName = "api_keys.json";
        public void Start()
        {
            this.MapTileOptions = new ObservableCollection<MapTile>();
            this.GeneralElements = new GeneralElements
            {
                GameDescription = string.Empty,
                GameName = string.Empty,
                LevelDescription = string.Empty,
                LevelName = string.Empty,
            };
            this.GeneralElements.PropertyChanged += this.GeneralElementsPropertyChanged;
            this.MapConstraints = new MapConstraints
            {
                Height = 0,
                Width = 0,
                GameType = GameType.TopDown,
                GameGenre = string.Empty,
                DifficultyLevel = DifficultyLevel.Normal,
                HazardDensity = Density.Normal,
                CustomConstraints = string.Empty,
            };
            this.MapConstraints.PropertyChanged += this.MapConstraintsPropertyChanged;

            this.Output = new();

            this.promptUserData = new PromptUserData
            {
                GeneralElements = this.GeneralElements,
                MapTileOptions = this.MapTileOptions,
                MapConstraints = this.MapConstraints,
            };

            this.apiKeys = this.GetApiKeys();
            this.LlmClient = LlmClientFactory.CreateClient(LLMProviders.Providers.OpenAI, LLMProviders.OpenAIClients.Responses, this.apiKeys.GetValueOrDefault(LLMProviders.Providers.OpenAI.ToString()));
            this.Model = new()
            {
                SelectedModel = string.Empty,
            };

            if (this.LlmClient is LlmClientBase baseClient)
            {
                this.AvailableModels = baseClient.AllowedModels.ToArray();
                this.Model.SelectedModel = baseClient.Model;
                this.Model.PropertyChanged += this.ModelPropertyChanged;
            }

            // Initialize arrays for combo boxes
            this.GameTypeArray = Enum.GetValues<GameType>();
            this.DifficultyArray = Enum.GetValues<DifficultyLevel>();
            this.HazardLevelArray = Enum.GetValues<Density>();

            // ApiKeyUI init
            this.LlmProvidersArray = Enum.GetValues<LlmProviders>();

            this.FontProperties = new();
            this.CalculateOutputLineHeight();
        }

        private Dictionary<string, string> GetApiKeys()
        {
            try
            {
                var jsonString = File.ReadAllText(ApiKeyFileName);
                var jsonObject = JsonSerializer.Deserialize<ObservableCollection<ExternalServices.Clients.LlmApiKey>>(jsonString);
                var keysDict = new Dictionary<string, string>();
                foreach (var obj in jsonObject)
                {
                    var key = obj.Provider;
                    var value = obj.Key;
                    keysDict.TryAdd(key.ToString(), value);
                }

                if (keysDict.Count > 0)
                {
                    return keysDict;
                }
                else
                {
                    throw new Exception("ERROR: No kays found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: API Keys deserializing failed. Check the existence and/or format of the json file. Exception: {ex}");
                // Need to trigger the Api Keys window and await until it is closed
                // Window Open
                // await close
                var apiKeyWindow = new ApiKeysWindow();
                this.Show();
                apiKeyWindow.Owner = this;
                var result = apiKeyWindow.ShowDialog();

                if (result == true)
                {
                    return this.GetApiKeys();
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
