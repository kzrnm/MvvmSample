using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows;

namespace Kzrnm.MvvmSample.Wpf.Behaviors
{

    public class DialogMessage : RequestMessage<MessageBoxResult>
    {
        public string? Caption { get; init; }
        public string? Text { get; init; }
        public MessageBoxButton MessageBoxButton { get; init; }
        public MessageBoxImage MessageBoxImage { get; init; }
    }
}
