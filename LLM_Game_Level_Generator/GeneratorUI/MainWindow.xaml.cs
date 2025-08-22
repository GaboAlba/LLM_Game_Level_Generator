namespace GeneratorUI
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Start();
            this.DataContext = this;
        }
    }
}
