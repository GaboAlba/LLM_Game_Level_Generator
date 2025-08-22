namespace GeneratorUI
{
    using System.Windows;
    using System.Windows.Forms;

    public partial class MainWindow
    {

        /// <summary>
        /// Workflow to create a new tile type list item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MapTileOptions.Add(new UserInput.MapTile
            {
                TileCharacter = string.Empty,
                TileDescription = string.Empty,
                TileName = string.Empty
            });
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
        /// Workflow to save the file as an specifi name
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
    }
}
