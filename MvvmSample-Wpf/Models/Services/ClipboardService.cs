using System.Windows;

namespace Kzrnm.MvvmSample.Wpf.Models.Services
{
    public interface IClipboardService
    {
        void SetText(string text);
    }
    public class ClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
