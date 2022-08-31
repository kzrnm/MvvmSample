using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kzrnm.MvvmSample.Wpf.Behaviors
{
    public class DialogBehavior : Behavior<Control>, IRecipient<DialogMessage>
    {
        public static readonly DependencyProperty TokenProperty =
            DependencyProperty.Register(
                nameof(Token),
                typeof(Guid),
                typeof(DialogBehavior),
                new PropertyMetadata(Guid.Empty));
        public Guid Token
        {
            get => (Guid)GetValue(TokenProperty);
            set => SetValue(TokenProperty, value);
        }

        protected override void OnAttached()
        {
            if (Token == Guid.Empty)
                WeakReferenceMessenger.Default.Register(this);
            else
                WeakReferenceMessenger.Default.Register(this, Token);
            AssociatedObject.Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object? sender, EventArgs e)
        {
            Detach();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Unloaded -= OnUnloaded;
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }
        protected Window GetWindow() => Window.GetWindow(AssociatedObject);
        public void Receive(DialogMessage message)
        {
            var text = message.Text ?? "";
            var caption = message.Caption ?? "";
            var result = MessageBox.Show(GetWindow(), text, caption, message.MessageBoxButton, message.MessageBoxImage);
            message.Reply(result);
        }
    }
}
