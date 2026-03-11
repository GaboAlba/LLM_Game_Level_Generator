namespace MAUI_UI
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage())
            {
                Title = "LLM Game Level Generator",
                Width = 1920,
                Height = 1080,
            };
        }
    }
}
