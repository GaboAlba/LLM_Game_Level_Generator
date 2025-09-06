namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class MapTile : INotifyPropertyChanged, IDataErrorInfo
    {
        public string TileName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.TileName));
            }
        } = string.Empty;

        public string TileCharacter
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    this.OnPropertyChanged(nameof(this.TileCharacter));
                }
            }
        } = string.Empty;

        public string TileDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.TileDescription));
            }
        } = string.Empty;

        // Validation support
        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(this.TileCharacter))
                {
                    if (this.ValidateCharacter != null && !this.ValidateCharacter(this.TileCharacter))
                    {
                        return "Character is already used or invalid.";
                    }
                }

                return string.Empty;
            }
        }

        public Func<string, bool>? ValidateCharacter { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
