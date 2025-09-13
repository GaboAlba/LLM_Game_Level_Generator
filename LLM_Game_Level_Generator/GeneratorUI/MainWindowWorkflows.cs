namespace GeneratorUI
{
    using GeneratorViewModel;

    using LLMGenCoreLib.PromptTemplates;

    using System.ComponentModel;
    using System.IO;
    using System.Text.Json;
    using System.Windows;
    using System.Windows.Forms;

    public partial class MainWindow
    {

        private bool isCurrentCharacter;

        /// <summary>
        /// Workflow to create a new tile type list item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tile = new MapTile
            {
                TileCharacter = string.Empty,
                TileDescription = string.Empty,
                TileName = string.Empty,
                ValidateCharacter = c =>
                {
                    if (this.isCurrentCharacter)
                    {
                        this.isCurrentCharacter = false;
                        return true;
                    }
                    
                    return string.IsNullOrEmpty(c) || ( !this.usedCharacters.Contains(c) && c.Length == 1 );
                }
            };
            tile.PropertyChanged += this.TilePropertyChanged;
            this.MapTileOptions.Add(tile);
        }

        private void deleteTileButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTileList = new List<MapTile>(this.mapTileList.SelectedItems.Cast<MapTile>());
            foreach (var selectedTile in selectedTileList)
            {
                this.MapTileOptions.Remove(selectedTile);
                this.usedCharacters.Remove(selectedTile.TileCharacter);
                selectedTile.PropertyChanged -= this.TilePropertyChanged;
            }
        }

        /// <summary>
        /// Workflow to create a new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsCurrentSessionModifiedAndNotSaved())
            {
                // TODO: Trigger an additional dialog for the user to choose wheter to save or discard changes
                this.saveButton_Click(sender, e);
            }

            this.Reset();
        }

        /// <summary>
        /// Workflow to open an existing file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFIleButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Check if current file has been saved first

            if (this.IsCurrentSessionModifiedAndNotSaved())
            {
                // TODO: Trigger an additional dialog for the user to choose wheter to save or discard changes
                this.saveButton_Click(sender, e);
            }

            var fileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                DefaultExt = ".json",
                ShowPreview = true,
                Filter = "Json Files|*.json",
            };

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var fileContent = File.ReadAllText(fileDialog.FileName);
            try
            {
                var tempPromptUserData = JsonSerializer.Deserialize<PromptUserData>(fileContent);
                if (tempPromptUserData != null)
                {
                    this.UpdateAllFields(tempPromptUserData);
                }
                else
                {
                    throw new FileLoadException("Null File tried to be loaded");
                }

                this.saveFilePath = fileDialog.FileName;
            }
            catch (Exception ex)
            {
                // TODO: Add exception message to the UI.
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Workflow to save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.saveFilePath))
            {
                this.saveAsButton_Click(sender, e);
                return;
            }

            this.SaveFileAsJson(this.saveFilePath, this.promptUserData);
        }

        /// <summary>
        /// Workflow to save the file as an specific name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog()
            {
                RestoreDirectory = true,
                CheckWriteAccess = true,
                DefaultExt = ".json",
                Filter = "Json Files|*.json"
            };

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            this.SaveFileAsJson(fileDialog.FileName, this.promptUserData);
            this.saveFilePath = fileDialog.FileName;
        }

        /// <summary>
        /// Workflow to generate the LLM Response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var prompt = PromptGroundingDataInjector.CreatePrompt(this.promptUserData);
            var messages = this.LlmClient.BuildMessages(prompt);
            var request = this.LlmClient.BuildRequest(messages);
            var response = await this.LlmClient.GetResponseAsync(request);
        }

        private void Reset()
        {
            this.UpdateAllFields(new PromptUserData());
            this.usedCharacters.Clear();
            this.saveFilePath = string.Empty;
            this.savedInCurrentSession = false;
            this.modifiedInCurrentSession = false;
        }

        private void TilePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!this.modifiedInCurrentSession)
            {
                this.modifiedInCurrentSession = true;
                this.savedInCurrentSession = false;
            }

            if (sender is MapTile tile && e.PropertyName == nameof(MapTile.TileCharacter))
            {
                if (!string.IsNullOrEmpty(tile.TileCharacter) && tile.TileCharacter.Length == 1 && this.usedCharacters.Add(tile.TileCharacter))
                {
                    this.isCurrentCharacter = true;
                    this.usedCharacters = this.MapTileOptions.Select(tile => tile.TileCharacter).ToHashSet();
                }
                else
                {
                    Console.WriteLine($"ERROR: Trying to add {tile.TileCharacter} to HashSet is invalid");
                }
            }
        }

        private void GeneralElementsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is GeneralElements generalElements && !this.modifiedInCurrentSession)
            {
                this.modifiedInCurrentSession = true;
                this.savedInCurrentSession = false;
            }
        }

        private void MapConstraintsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is MapConstraints mapConstraints && !this.modifiedInCurrentSession)
            {
                this.modifiedInCurrentSession = true;
                this.savedInCurrentSession = false;
            }
        }

        private void UpdateAllFields(PromptUserData promptUserData)
        {
            this.GeneralElements.CopyFrom(promptUserData.GeneralElements);
            this.MapConstraints.CopyFrom(promptUserData.MapConstraints);
            this.MapTileOptions.Clear();
            foreach (var tile in promptUserData.MapTileOptions)
            {
                this.MapTileOptions.Add(tile);
            }

            // Reset the modified flag after loading
            this.modifiedInCurrentSession = false;
        }

        private bool IsCurrentSessionModifiedAndNotSaved()
        {
            if (this.modifiedInCurrentSession && !this.savedInCurrentSession)
            {
                return true;
            }

            return false;
        }

        // TODO: Move this to it's own class
        private void SaveFileAsJson(string filePath, PromptUserData promptUserData)
        {
            var jsonString = JsonSerializer.Serialize(promptUserData);
            File.WriteAllText(filePath, jsonString);
            this.savedInCurrentSession = true;
        }

    }
}
