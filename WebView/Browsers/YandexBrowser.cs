using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class YandexBrowser : Browser
{
    public YandexBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yandex\\YandexBrowser\\Application\\browser.exe"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Yandex.app/Contents/MacOS/Yandex"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
        }
    }

    public override void OpenApp(BrowserModes modes, string url, int left, int top, int width, int height)
    {
        Open($"-app=\"{url}\" --window-position={left},{top} --window-size={width},{height}");
    }
}
