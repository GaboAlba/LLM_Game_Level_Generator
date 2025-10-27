namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class FontProperties : INotifyPropertyChanged
    {
        public double OutputLineHeight
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.OutputLineHeight));
            }
        } = 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
