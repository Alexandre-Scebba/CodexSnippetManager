using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnippetManager.Data;
using System;
using System.Windows;

namespace SnippetManager
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            configurationBuilder.AddJsonFile(@"C:\Users\xdimi\source\repos\SnippetManager\SnippetManager\appsettings.json");
            var configuration = configurationBuilder.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ApplicationDbContext>(provider =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new ApplicationDbContext(connectionString);
            });
            services.AddTransient<MainWindow>();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}