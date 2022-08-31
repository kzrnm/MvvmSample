using CommunityToolkit.Mvvm.DependencyInjection;
using Kzrnm.MvvmSample.Wpf.Models.Services;
using Kzrnm.MvvmSample.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Windows;

namespace Kzrnm.MvvmSample.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<HttpClient>()
                .AddSingleton<IClipboardService, ClipboardService>()
                .AddSingleton<IWebTimeService, WebTimeService>()
                .AddTransient<MainWindowViewModel>()
                .BuildServiceProvider());
        }
    }
}
