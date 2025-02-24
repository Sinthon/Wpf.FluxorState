﻿using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Windows;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.Store;
using Wpf.FluxorState.ViewModels;
using Wpf.FluxorState.Views;

namespace Wpf.FluxorState;

public partial class App : Application
{
    private IServiceProvider _serviceProvider = default!;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var services = new ServiceCollection();

        ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();

        try
        {
            var store = _serviceProvider.GetRequiredService<IStore>();
            await store.InitializeAsync();

            var viewFactory = _serviceProvider.GetRequiredService<IViewFactory>();
            var mainWindow = viewFactory.CreateView<MainWindowViewModel>() as MainWindow;
            mainWindow.Show();
        }
        catch (Exception ex)
        {

        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddFluxorStore();
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddSingleton<IViewFactory, ViewFactory>();
        services.AddSingleton<IDialogManager, DialogManager>();
    }
}

