#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace GeneratorUI
{
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;

    using GeneratorViewModel;

    using LLMGenCoreLib.PromptTemplates;

    using OpenAI.Responses;

    using System.ComponentModel;
    using System.IO;
    using System.Text.Json;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Media;

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
                MinimumNumberOfTiles = null,
                MaximumNumberOfTiles = null,
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

        private void saveMapButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog()
            {
                RestoreDirectory = true,
                CheckWriteAccess = true,
                DefaultExt = ".txt",
                Filter = "Text Files|*.txt",
            };

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            File.WriteAllText(fileDialog.FileName, this.Output.GeneratedMap);
        }

        /// <summary>
        /// Workflow to generate the LLM Response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string responseMap = string.Empty;
            try
            {
                this.progressBar1.IsIndeterminate = true;
                var prompt = PromptGroundingDataInjector.CreatePrompt(this.promptUserData);
                var messages = this.LlmClient.BuildMessages(prompt);
                var request = this.LlmClient.BuildRequest(messages, false);

                var responsesClient = this.LlmClient as ResponsesClient;

                var response = new LLMResponse { Id = "0" };
                var ui = SynchronizationContext.Current;
                var progress = new Progress<string>(text =>
                {
                    this.Output.ReasoningSummary = text;
                });

                if (request.Stream)
                {
                    response = await this.LlmClient.GetResponseStreamAsync(request, progress);
                }
                else
                {
                    response = await this.LlmClient.GetResponseAsync(request, progress);
                }

                
                if (response != null && 
                    response?.Error?.Code == null &&
                    response?.Error?.Message == null && 
                    response?.OutputText != null)
                {
                    responseMap = response.OutputText;
                }
                else
                {
                    throw new NullReferenceException(
                        $"Either the LLM response, or the output are null. Or there was an error" +
                        $"Error: {response?.Error}, " +
                        $"Output: {response?.OutputText}");
                }
            }
            catch (Exception ex)
            {
                responseMap = ex.Message;
                Console.WriteLine(ex);
            }
            finally
            {
                this.Output.GeneratedMap = responseMap;
                this.progressBar1.IsIndeterminate = false;
            }
        }

        private async void OptimizeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.progressBar1.IsIndeterminate = true;
                var prompt = PromptGroundingDataInjector.CreateOptimizerPrompt(this.promptUserData); // Change to Optimizer Prompt
                var messages = this.LlmClient.BuildMessages(prompt);
                var request = this.LlmClient.BuildRequest(messages);

                var responsesClient = this.LlmClient as ResponsesClient;
                responsesClient.ResponseTextFormat = ResponseTextFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "2d-grid-optimizer-format",
                    jsonSchema: BinaryData.FromString(PromptUserData.GetJsonSchema()),
                    jsonSchemaIsStrict: true);

                var ui = SynchronizationContext.Current;
                var progress = new Progress<string>(text =>
                {
                    this.Output.ReasoningSummary = text;
                });
                var response = await responsesClient.GetResponseAsync(request, progress);

                var jsonString = response.OutputText;
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var newPromptData = JsonSerializer.Deserialize<PromptUserData>(jsonString);

                    // Need to sync the constraints as these are not optimized, except for the custom constraints
                    var customConstraints = newPromptData.MapConstraints.CustomConstraints;
                    newPromptData.MapConstraints = this.MapConstraints;
                    newPromptData.MapConstraints.CustomConstraints = customConstraints;

                    this.UpdateAllFields(newPromptData);
                    this.modifiedInCurrentSession = true;
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                this.progressBar1.IsIndeterminate = false;
            }
        }

        private void RunEvalButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            this.FontSize *= 1.1;
            this.CalculateOutputLineHeight();
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            this.FontSize *= 0.9;
            this.CalculateOutputLineHeight();
        }

        private void Reset()
        {
            this.UpdateAllFields(new PromptUserData());
            this.Output.GeneratedMap = string.Empty;
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
                else if (string.IsNullOrEmpty(tile.TileCharacter))
                {
                    this.isCurrentCharacter = false;
                    
                    // Only add all non empty values
                    this.usedCharacters = this.MapTileOptions
                        .Select(tile => tile.TileCharacter)
                        .Where(tileCharacter => !string.IsNullOrEmpty(tileCharacter))
                        .ToHashSet();
                }
                else if (tile.TileCharacter.Length > 1)
                {
                    this.isCurrentCharacter = false;
                    
                    // Do not add multi character to the used character
                    this.usedCharacters = this.MapTileOptions
                        .Select(tile => tile.TileCharacter)
                        .Where(tileCharacter => tileCharacter != tile.TileCharacter)
                        .ToHashSet();
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

        private void CalculateOutputLineHeight()
        {
            var typeface = new Typeface(new FontFamily("Consolas"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            var formattedText = new FormattedText(
                "W",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Windows.FlowDirection.LeftToRight,
                typeface,
                this.outputTextBlock.FontSize,
                Brushes.Black,
                1.0);

            this.FontProperties.OutputLineHeight = formattedText.Width;
        }
    }
}

#pragma warning restore OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
