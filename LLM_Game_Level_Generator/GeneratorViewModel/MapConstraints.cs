namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class MapConstraints : INotifyPropertyChanged
    {
        public required int Width
        { 
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Width));
            }
        }

        public required int Height
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Height));
            }
        }

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
