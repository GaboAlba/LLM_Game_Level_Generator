namespace GeneratorUI
{
    using Microsoft.Extensions.DependencyInjection;

    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            this.ConfigureServices(serviceCollection);

            this.serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = this.serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddSingleton<MainWindow>();
        }

        private void OnExit(object sender, EventArgs e)
        {
            if (this.serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

}
