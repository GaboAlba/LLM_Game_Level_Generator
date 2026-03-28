namespace GeneratorUI
{
    using GeneratorViewModel;

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Text.Json;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ApiKeysWindow : Window
    {
        public Array LlmProvidersArray { get; set; }
        public ObservableCollection<GeneratorViewModel.LlmApiKey> LlmApiKeys { get; set; }

        // TODO: Need to change this to relative path for distribution.
        private const string ApiKeyFileName = "api_keys.json";

        public ApiKeysWindow()
        {
            this.InitializeComponent();
            this.LlmProvidersArray = Enum.GetValues<LlmProviders>();
            if (File.Exists(ApiKeyFileName))
            {
                var jsonString = File.ReadAllText(ApiKeyFileName);
                this.LlmApiKeys = JsonSerializer.Deserialize<ObservableCollection<LlmApiKey>>(jsonString) ?? [];
            }
            else
            {
                this.LlmApiKeys = new ObservableCollection<GeneratorViewModel.LlmApiKey>();
            }

            this.HideKeysOnStartup();

            this.DataContext = this;
        }

        private void AddApiKey_Click(object sender, RoutedEventArgs e)
        {
            var key = new LlmApiKey
            {
                Provider = LlmProviders.OpenAI,
                Key = string.Empty,
                IsKeyHidden = true
            };
            key.PropertyChanged += this.KeyPropertyChanged;
            this.LlmApiKeys.Add(key);
        }
        private async void SaveApiKeys_Click(object sender, RoutedEventArgs e)
        {
            var jsonString = JsonSerializer.Serialize(this.LlmApiKeys);
            await File.WriteAllTextAsync(ApiKeyFileName, jsonString);

            // Animate the save action
            this.savedKeysTextBlock.Visibility = Visibility.Visible;
            while (this.savedKeysTextBlock.Opacity > 0.05d)
            {
                this.savedKeysTextBlock.Opacity -= 0.05d;
                await Task.Delay(33); // ~30FPS
            }

            this.savedKeysTextBlock.Visibility = Visibility.Hidden;
            this.savedKeysTextBlock.Opacity = 1;
        }

        private void KeyPropertyChanged(object? sender, PropertyChangedEventArgs e)
        { }

        private void ApiKeyOkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ApiKeyCancelButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ShowApiKey_Click(object sender, RoutedEventArgs e)
        {
            foreach (var keyObject in this.LlmApiKeys)
            {
                if (sender is Button button && button.DataContext == keyObject)
                {
                    keyObject.IsKeyHidden = !keyObject.IsKeyHidden; // Invert state
                }
            }
        }

        private void HideKeysOnStartup()
        {
            foreach (var keyObject in this.LlmApiKeys)
            {
                keyObject.IsKeyHidden = true;
            }
        }
    }
}
