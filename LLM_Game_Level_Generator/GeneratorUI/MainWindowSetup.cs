namespace GeneratorUI
{
    using ExternalServices.Clients;
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

        private HashSet<string> usedCharacters = new HashSet<string>();
        private ILlmClient LlmClient;
        private readonly Dictionary<string, string> apiKeys = GetApiKeys();
        private PromptUserData promptUserData;
        private string saveFilePath;
        private bool savedInCurrentSession = false;
        private bool modifiedInCurrentSession = false;

        // TODO: Need to change this to relative path for distribution.
        private const string apiKeysDir = "C:\\Users\\Gabriel\\OneDrive\\Documents\\GitHub\\LLM_Game_Level_Generator\\LLM_Game_Level_Generator\\ExternalServices\\api_keys.json";
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
                MaxJumpHeight = 0,
                MaxJumpWidth = 0,
                MinNumberOfObstacles = 0,
                MaxNumberOfObstacles = 0,
                CustomConstraints = string.Empty,
            };
            this.MapConstraints.PropertyChanged += this.MapConstraintsPropertyChanged;

            this.promptUserData = new PromptUserData
            {
                GeneralElements = this.GeneralElements,
                MapTileOptions = this.MapTileOptions,
                MapConstraints = this.MapConstraints,
            };

            this.LlmClient = LlmClientFactory.CreateClient(LLMProviders.Providers.OpenAI, LLMProviders.OpenAIClients.Responses, this.apiKeys.GetValueOrDefault(LLMProviders.Providers.OpenAI.ToString()));
        }

        private static Dictionary<string, string> GetApiKeys()
        {
            try
            {
                var jsonString = File.ReadAllText(apiKeysDir);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: API Keys deserializing failed. Check the existence and/or format of the json file. Exception: {ex}");
                throw;
            }
        }
    }
}
