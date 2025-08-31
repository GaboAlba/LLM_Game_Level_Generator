namespace GeneratorUI
{
    using GeneratorUI.Utils;
    using GeneratorUI.ViewModel;

    using System.Collections.ObjectModel;

    public partial class MainWindow
    {
        public ObservableCollection<MapTile> MapTileOptions { get; set; }
        public GeneralElements GeneralElements { get; set; }
        public MapConstraints MapConstraints { get; set; }

        private HashSet<string> usedCharacters = new HashSet<string>();
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
        }
    }
}
