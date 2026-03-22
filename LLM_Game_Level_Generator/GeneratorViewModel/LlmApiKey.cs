namespace GeneratorViewModel
{
    using System.ComponentModel;
    using System.Text.Json.Serialization;

    public class LlmApiKey : INotifyPropertyChanged
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LlmProviders Provider
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Provider));
            }
        }

        public string Key
        {
            get;
            set
            {
                field = value;
                this.OnPropertyChanged(nameof(this.Key));
            }
        }

        public void CopyFrom(LlmApiKey mapConstraints)
        {
            this.Provider = mapConstraints.Provider;
            this.Key = mapConstraints.Key;
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
