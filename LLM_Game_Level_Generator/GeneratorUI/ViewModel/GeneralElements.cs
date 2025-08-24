
namespace GeneratorUI.ViewModel
{
    using System.ComponentModel;

    public class GeneralElements : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public required string GameDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameDescription));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public required string GameName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public required string LevelDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.LevelDescription));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public required string LevelName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.LevelName));
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
