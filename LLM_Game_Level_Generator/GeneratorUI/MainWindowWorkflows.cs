namespace GeneratorUI
{
    using GeneratorViewModel;

    using System.ComponentModel;
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

        }

        /// <summary>
        /// Workflow to open an existing file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFIleButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Check if current file has been saved first

            var fileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
            };

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            // Handle the opening by creating the load file logic.
        }

        /// <summary>
        /// Workflow to save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

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
            };

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            // Handle saving
        }

        /// <summary>
        /// Workflow to generate the LLM Response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var messages = this.LlmClient.BuildMessages(string.Empty);
            var request = this.LlmClient.BuildRequest(messages);
            var response = await this.LlmClient.GetResponseAsync(request);
        }

        private void TilePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
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
    }
}
