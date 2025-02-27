using Fluxor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Windows;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.Infrastructure;
using Wpf.FluxorState.Infrastructure.SignalR;
using Wpf.FluxorState.Store;
using Wpf.FluxorState.ViewModels;
using Wpf.FluxorState.Views;

namespace Wpf.FluxorState;

public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource
            {
                Path = "appsettings.json",
                Optional = false,
                ReloadOnChange = true
            })
            .Build();

        ConfigureServices(services, configuration);

        var _serviceProvider = services.BuildServiceProvider();

        /// Init store
        await _serviceProvider
            .GetRequiredService<IStore>()
            .InitializeAsync();

        /// Init SignalR
        await _serviceProvider
            .GetRequiredService<SignalRManager>()
            .StartConnectionAsync(configuration
                .GetConnectionString("SignalR")!);

        /// Init main window
        var rootView = _serviceProvider
            .GetRequiredService<IViewFactory>()
            .CreateView<MainWindowViewModel>();

        if (rootView is MainWindow mainWindow)
        {
            mainWindow.Show();
        }
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluxorStore();
        services.AddInfrastructure(configuration);
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddSingleton<IViewFactory, ViewFactory>();
        services.AddSingleton<IDialogManager, DialogManager>();
    }
}

