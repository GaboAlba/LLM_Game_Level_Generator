namespace GeneratorUI.ViewModel
{
    using System.ComponentModel;

    public class MapTile : INotifyPropertyChanged
    {
        public required string TileName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.TileName));
            }
        }

        public required string TileCharacter
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.TileCharacter));
            }
        }

        public required string TileDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.TileDescription));
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
