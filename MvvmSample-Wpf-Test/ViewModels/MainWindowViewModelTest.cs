using CommunityToolkit.Mvvm.Messaging;
using Kzrnm.MvvmSample.Wpf.Behaviors;
using Kzrnm.MvvmSample.Wpf.Models.Services;
using Moq;
using System.Threading.Tasks;

namespace Kzrnm.MvvmSample.Wpf.ViewModels
{
public class MainWindowViewModelTest
{
readonly object recipient = new object();

[Fact]
public void DialogYes()
{
    var webTimeServiceMock = new Mock<IWebTimeService>();
    var clipboardServiceMock = new Mock<IClipboardService>();

    lock (recipient)
    {
        try
        {
            WeakReferenceMessenger.Default.Register<DialogMessage>(recipient, (_, message) =>
            {
                message.Reply(System.Windows.MessageBoxResult.Yes);
            });
            var viewModel = new MainWindowViewModel(webTimeServiceMock.Object, clipboardServiceMock.Object);
            Assert.Null(viewModel.CurrentDateTime);

            viewModel.CopyCurrentDateTimeCommand.Execute(null);
            clipboardServiceMock.Verify(s => s.SetText(It.IsAny<string>()), Times.Never());


            var clock = new Models.WorldClock
            {
                TimeZoneName = "UTC",
                CurrentFileTime = 133000000000000000,
            };
            viewModel.CurrentDateTime = clock;

            viewModel.CopyCurrentDateTimeCommand.Execute(null);
            clipboardServiceMock.Verify(s => s.SetText(clock.DateTime.ToString()), Times.Once());
        }
        finally
        {
            WeakReferenceMessenger.Default.UnregisterAll(recipient);
        }
    }
}

[Fact]
public void DialogNo()
{
    var webTimeServiceMock = new Mock<IWebTimeService>();
    var clipboardServiceMock = new Mock<IClipboardService>();

    lock (recipient)
    {
        try
        {
            WeakReferenceMessenger.Default.Register<DialogMessage>(recipient, (_, message) =>
            {
                message.Reply(System.Windows.MessageBoxResult.No);
            });
            var viewModel = new MainWindowViewModel(webTimeServiceMock.Object, clipboardServiceMock.Object);
            Assert.Null(viewModel.CurrentDateTime);

            viewModel.CopyCurrentDateTimeCommand.Execute(null);
            clipboardServiceMock.Verify(s => s.SetText(It.IsAny<string>()), Times.Never());

            var clock = new Models.WorldClock
            {
                TimeZoneName = "UTC",
                CurrentFileTime = 133000000000000000,
            };
            viewModel.CurrentDateTime = clock;

            viewModel.CopyCurrentDateTimeCommand.Execute(null);
            clipboardServiceMock.Verify(s => s.SetText(It.IsAny<string>()), Times.Never());
        }
        finally
        {
            WeakReferenceMessenger.Default.UnregisterAll(recipient);
        }
    }
}
[Fact]
public async Task GetDateTime()
{
    var webTimeServiceMock = new Mock<IWebTimeService>();
    var clipboardServiceMock = new Mock<IClipboardService>();

    webTimeServiceMock.Setup(s => s.GetTimeAsync())
        .ReturnsAsync(new Models.WorldClock
        {
            TimeZoneName = "UTC",
            CurrentFileTime = 133000000000000000,
        });

    var viewModel = new MainWindowViewModel(webTimeServiceMock.Object, clipboardServiceMock.Object);
    Assert.Null(viewModel.CurrentDateTime);
    await viewModel.GetDateTimeCommand.ExecuteAsync(null);
    Assert.NotNull(viewModel.CurrentDateTime);
    Assert.Equal(133000000000000000, viewModel.CurrentDateTime.CurrentFileTime);
}
}
}