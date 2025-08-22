namespace GeneratorUI
{
    using GeneratorUI.UserInput;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    public partial class MainWindow
    {
        public ObservableCollection<MapTile> MapTileOptions { get; set; }
        public GeneralElements GeneralElements { get; set; }
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
        }
    }
}
