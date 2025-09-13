
namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class GeneralElements : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public string GameDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameDescription));
            }
        } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string GameName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GameName));
            }
        } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string LevelDescription
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.LevelDescription));
            }
        } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string LevelName
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.LevelName));
            }
        } = string.Empty;

        public void CopyFrom(GeneralElements generalElements)
        {
            this.GameName = generalElements.GameName;
            this.GameDescription = generalElements.GameDescription;
            this.LevelName = generalElements.LevelName;
            this.LevelDescription = generalElements.LevelDescription;
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
