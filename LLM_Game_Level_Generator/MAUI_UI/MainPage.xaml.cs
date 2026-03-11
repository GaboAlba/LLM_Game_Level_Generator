namespace MAUI_UI
{
    public partial class MainPage : ContentPage
    {
        private Grid[] tabPanels;
        private Button[] tabButtons;

        public MainPage()
        {
            this.InitializeComponent();
            this.Start();
            this.BindingContext = this;
            this.InitializeTabs();
        }

        private void InitializeTabs()
        {
            this.tabPanels = [this.tabPanel0, this.tabPanel1, this.tabPanel2, this.tabPanel3, this.tabPanel4];
            this.tabButtons = [this.tabBtn0, this.tabBtn1, this.tabBtn2, this.tabBtn3, this.tabBtn4];
        }

        private void ShowFileFlyout_Clicked(object sender, EventArgs e) =>
            FlyoutBase.GetContextFlyout((BindableObject)sender);

        private void ShowViewFlyout_Clicked(object sender, EventArgs e) =>
            FlyoutBase.GetContextFlyout((BindableObject)sender);

        private void ShowHelpFlyout_Clicked(object sender, EventArgs e) =>
            FlyoutBase.GetContextFlyout((BindableObject)sender);

        private void TabButton_Clicked(object? sender, EventArgs e)
        {
            if (sender is not Button clickedButton || !int.TryParse(clickedButton.ClassId, out var index))
                return;

            for (int i = 0; i < this.tabPanels.Length; i++)
            {
                this.tabPanels[i].IsVisible = i == index;
                this.tabButtons[i].Style = i == index
                    ? (Style)this.Resources["ActiveTabStyle"]
                    : (Style)this.Resources["InactiveTabStyle"];
            }
        }
    }
}
