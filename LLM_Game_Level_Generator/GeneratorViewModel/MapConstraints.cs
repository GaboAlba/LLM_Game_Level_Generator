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

        public int? MaxJumpHeight
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MaxJumpHeight));
            }
        }

        public int? MaxJumpWidth
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MaxJumpWidth));
            }
        }

        public int? MinNumberOfObstacles
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MinNumberOfObstacles));
            }
        }

        public int? MaxNumberOfObstacles
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.MaxNumberOfObstacles));
            }
        }

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
            this.MaxJumpHeight = mapConstraints.MaxJumpHeight;
            this.MaxJumpWidth = mapConstraints.MaxJumpWidth;
            this.MinNumberOfObstacles = mapConstraints.MinNumberOfObstacles;
            this.MaxNumberOfObstacles = mapConstraints.MaxNumberOfObstacles;
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
