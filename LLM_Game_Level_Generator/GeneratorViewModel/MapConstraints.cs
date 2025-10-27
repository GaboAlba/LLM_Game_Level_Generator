namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class MapConstraints : INotifyPropertyChanged
    {
        public int Width
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Width));
            }
        } = 0;

        public int Height
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Height));
            }
        } = 0;

        public GameType GameType
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameType));
            }
        } = GameType.TopDown;

        public string? GameGenre
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameGenre));
            }
        }

        public DifficultyLevel DifficultyLevel
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.DifficultyLevel));
            }
        } = DifficultyLevel.Normal;

        public Density HazardDensity
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.HazardDensity));
            }
        } = Density.Normal;

        public string? CustomConstraints
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.CustomConstraints));
            }
        }

        public void CopyFrom(MapConstraints mapConstraints)
        {
            this.Width = mapConstraints.Width;
            this.Height = mapConstraints.Height;
            this.CustomConstraints = mapConstraints.CustomConstraints;
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
