namespace GeneratorViewModel
{
    using System.Collections.ObjectModel;

    public class PromptUserData
    {
        public required GeneralElements GeneralElements { get; set; }

        public required ObservableCollection<MapTile> MapTileOptions { get; set; }

        public required MapConstraints MapConstraints { get; set; }
    }
}
