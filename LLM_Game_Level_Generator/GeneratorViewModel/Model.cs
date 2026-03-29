namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class Model : INotifyPropertyChanged
    {
        public string? SelectedModel
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.SelectedModel));
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
