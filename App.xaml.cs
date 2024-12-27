using FinancialManager.Common;
using FinancialManager.CustomUserControls;
using FinancialManager.Storage;
using FinancialManager.ViewModels;
using FinancialManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FinancialManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Настройка DI-контейнера
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceLocator.Initialize(ServiceProvider);

            // Инициализация главного окна
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация View
            services.AddSingleton<MainWindow>();
            services.AddSingleton<BudgetPlanning>();
            services.AddSingleton<IncomeAndExpenses>();
            services.AddTransient<StatisticsAndReports>();
            
            //view models
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<NavBarViewModel>();
            services.AddSingleton<IncomeAndExpensesViewModel>();
            services.AddSingleton<BudgetViewModel>();
            services.AddTransient<StatisticsAndReportsViewModel>();

            //services
            services.AddSingleton<CurrentPage>();
            services.AddTransient<StorageService>();

            // Регистрация UserControl
            services.AddSingleton<NavBar>();
        }

        public static T GetService<T>()
        {
            return ServiceLocator.ServiceProvider.GetService<T>();
        }
    }

    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider;

      
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }

}
