namespace GeneratorViewModel
{
    using System.Collections.ObjectModel;

    public class PromptUserData
    {
        public GeneralElements GeneralElements { get; set; } = new();

        public ObservableCollection<MapTile> MapTileOptions { get; set; } = new();

        public MapConstraints MapConstraints { get; set; } = new();
    }
}
