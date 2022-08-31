using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kzrnm.MvvmSample.Wpf.Behaviors;
using Kzrnm.MvvmSample.Wpf.Models;
using Kzrnm.MvvmSample.Wpf.Models.Services;
using System.Threading.Tasks;

namespace Kzrnm.MvvmSample.Wpf.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel(IWebTimeService gitHubService, IClipboardService clipboardService)
        {
            WebTimeService = gitHubService;
            ClipboardService = clipboardService;
        }

        private IWebTimeService WebTimeService { get; }
        private IClipboardService ClipboardService { get; }

        [ObservableProperty]
        private WorldClock? _CurrentDateTime;
        // [ObservableProperty] がついていると ↓ のようなコードが CommunityToolkit によって自動生成される
        // public WorldClock? CurrentDateTime
        // {
        //    set
        //    {
        //         if (!System.Collections.Generic.EqualityComparer<DateTime>.Default.Equals(_CurrentDateTime, value))
        //         {
        //             _CurrentDateTime = value;
        //             PropertyChanged?.Invoke(new PropertyChangedEventArgs("CurrentDateTime"));
        //         }
        //    }
        //    get => _CurrentDateTime;
        // }

        [RelayCommand]
        private async Task GetDateTime()
        {
            CurrentDateTime = await WebTimeService.GetTimeAsync();
        }

        [RelayCommand]
        private void CopyCurrentDateTime()
        {
            var dialogResult = WeakReferenceMessenger.Default.Send(new DialogMessage
            {
                Caption = "クリップボードにコピー",
                Text = "クリップボードにコピーしますか？",
                MessageBoxButton = System.Windows.MessageBoxButton.YesNo,
                MessageBoxImage = System.Windows.MessageBoxImage.Question,
            });
            if (dialogResult.HasReceivedResponse
                    && dialogResult.Response == System.Windows.MessageBoxResult.Yes
                    && CurrentDateTime?.DateTime is { } time)
            {
                ClipboardService.SetText(time.ToString());
            }
        }
    }
}
