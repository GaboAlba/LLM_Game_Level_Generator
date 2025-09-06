namespace GeneratorUI
{
    using ExternalServices.Clients;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    using GeneratorUI.Utils;
    
    using GeneratorViewModel;

    using System.Collections.ObjectModel;

    public partial class MainWindow
    {
        public ObservableCollection<MapTile> MapTileOptions { get; set; }
        public GeneralElements GeneralElements { get; set; }
        public MapConstraints MapConstraints { get; set; }

        private HashSet<string> usedCharacters = new HashSet<string>();
        private ILlmClient LlmClient;
        private readonly Dictionary<string, string> apiKeys = new Dictionary<string, string>();
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
            this.LlmClient = LlmClientFactory.CreateClient(LLMProviders.Providers.OpenAI, LLMProviders.OpenAIClients.Responses, this.apiKeys.GetValueOrDefault(LLMProviders.Providers.OpenAI.ToString()));
        }
    }
}
