namespace GeneratorViewModel
{
    using System.ComponentModel;

    public class Output : INotifyPropertyChanged
    {
        public string GeneratedMap 
        { 
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.GeneratedMap));
            }

        } = "The map will be generated here";

        public string ReasoningSummary
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.ReasoningSummary));
            }

        } = "The reasoning chain of thought will be generated here";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
