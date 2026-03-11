#pragma warning disable OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace MAUI_UI
{
    using CommunityToolkit.Maui.Storage;
    using ExternalServices.Clients.OpenAi;
    using ExternalServices.Contract;
    using GeneratorViewModel;
    using LLMGenCoreLib.PromptTemplates;
    using OpenAI.Responses;
    using System.ComponentModel;
    using System.Text;
    using System.Text.Json;

    public partial class MainPage
    {
        private bool isCurrentCharacter;

        /// <summary>
        /// Workflow to create a new tile type list item
        /// </summary>
        private void AddTile_Clicked(object? sender, EventArgs e)
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

                    return string.IsNullOrEmpty(c) || (!this.usedCharacters.Contains(c) && c.Length == 1);
                }
            };
            tile.PropertyChanged += this.TilePropertyChanged;
            this.MapTileOptions.Add(tile);
        }

        private void DeleteTile_Clicked(object? sender, EventArgs e)
        {
            var selectedTileList = new List<MapTile>(this.mapTileCollectionView.SelectedItems.Cast<MapTile>());
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
        private void NewFile_Clicked(object? sender, EventArgs e)
        {
            if (this.IsCurrentSessionModifiedAndNotSaved())
            {
                // TODO: Trigger an additional dialog for the user to choose whether to save or discard changes
                this.Save_Clicked(sender, e);
            }

            this.Reset();
        }

        /// <summary>
        /// Workflow to open an existing file
        /// </summary>
        private async void OpenFile_Clicked(object? sender, EventArgs e)
        {
            if (this.IsCurrentSessionModifiedAndNotSaved())
            {
                // TODO: Trigger an additional dialog for the user to choose whether to save or discard changes
                this.Save_Clicked(sender, e);
            }

            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Open Level File",
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".json" } },
                    }),
                });

                if (result == null)
                    return;

                var fileContent = File.ReadAllText(result.FullPath);
                var tempPromptUserData = JsonSerializer.Deserialize<PromptUserData>(fileContent);
                if (tempPromptUserData != null)
                {
                    this.UpdateAllFields(tempPromptUserData);
                }
                else
                {
                    throw new FileLoadException("Null File tried to be loaded");
                }

                this.saveFilePath = result.FullPath;
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Workflow to save the file
        /// </summary>
        private void Save_Clicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.saveFilePath))
            {
                this.SaveAs_Clicked(sender, e);
                return;
            }

            this.SaveFileAsJson(this.saveFilePath, this.promptUserData);
        }

        /// <summary>
        /// Workflow to save the file as a specific name
        /// </summary>
        private async void SaveAs_Clicked(object? sender, EventArgs e)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(this.promptUserData);
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

                var result = await FileSaver.Default.SaveAsync("level.json", stream, CancellationToken.None);
                if (result.IsSuccessful)
                {
                    this.saveFilePath = result.FilePath;
                    this.savedInCurrentSession = true;
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void SaveMap_Clicked(object? sender, EventArgs e)
        {
            try
            {
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.Output.GeneratedMap));
                await FileSaver.Default.SaveAsync("map.txt", stream, CancellationToken.None);
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Workflow to generate the LLM Response
        /// </summary>
        private async void Generate_Clicked(object? sender, EventArgs e)
        {
            string responseMap = string.Empty;
            try
            {
                this.activityIndicator.IsRunning = true;
                this.activityIndicator.IsVisible = true;

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
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                this.Output.GeneratedMap = responseMap;
                this.activityIndicator.IsRunning = false;
                this.activityIndicator.IsVisible = false;
            }
        }

        private async void Optimize_Clicked(object? sender, EventArgs e)
        {
            try
            {
                this.activityIndicator.IsRunning = true;
                this.activityIndicator.IsVisible = true;

                var prompt = PromptGroundingDataInjector.CreateOptimizerPrompt(this.promptUserData);
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
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                this.activityIndicator.IsRunning = false;
                this.activityIndicator.IsVisible = false;
            }
        }

        private void ZoomIn_Clicked(object? sender, EventArgs e)
        {
            this.zoomFactor *= 1.1;
            this.ApplyZoom();
        }

        private void ZoomOut_Clicked(object? sender, EventArgs e)
        {
            this.zoomFactor *= 0.9;
            this.ApplyZoom();
        }

        private void ApplyZoom()
        {
            var newSize = BaseOutputFontSize * this.zoomFactor;
            this.outputEditor.FontSize = newSize;
            this.reasoningEditor.FontSize = newSize;
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

        // TODO: Move this to its own class
        private void SaveFileAsJson(string filePath, PromptUserData promptUserData)
        {
            var jsonString = JsonSerializer.Serialize(promptUserData);
            File.WriteAllText(filePath, jsonString);
            this.savedInCurrentSession = true;
        }
    }

}


#pragma warning restore OPENAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
