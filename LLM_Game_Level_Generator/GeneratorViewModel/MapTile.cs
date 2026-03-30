namespace GeneratorViewModel
{
    using System.ComponentModel;
    using System.Text.Json.Serialization;

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

        public int? MinimumNumberOfTiles
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MinimumNumberOfTiles));
            }
        } = null;

        public int? MaximumNumberOfTiles
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MaximumNumberOfTiles));
            }
        } = null;

        // Validation support
        [JsonIgnore]
        public string Error => string.Empty;

        [JsonIgnore]
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

                if ((columnName == nameof(this.MinimumNumberOfTiles) ||  columnName == nameof(this.MaximumNumberOfTiles)))
                {
                    if (this.MinimumNumberOfTiles > this.MaximumNumberOfTiles)
                    {
                        return "The minimum number of tiles cannot be larger than the maximum";    
                    }

                    if (this.MaximumNumberOfTiles < 0 || this.MinimumNumberOfTiles < 0)
                    {
                        return "The number of tiles must be larger or equal to zero";
                    }
                }

                return string.Empty;
            }
        }

        [JsonIgnore]
        public Func<string, bool>? ValidateCharacter { get; set; }

        public void CopyFrom(MapTile tile)
        {
            this.TileName = tile.TileName;
            this.TileCharacter = tile.TileCharacter;
            this.TileDescription = tile.TileDescription;
            this.MinimumNumberOfTiles = tile.MinimumNumberOfTiles;
            this.MaximumNumberOfTiles = tile.MaximumNumberOfTiles;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private void OnPropertyChanged(string propertyName)
        {
            if (this.ValidateCharacter != null)
            {
                _ = this.ValidateCharacter(this.TileCharacter);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
