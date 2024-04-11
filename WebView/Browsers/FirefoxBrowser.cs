using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class FirefoxBrowser : Browser
{
    public FirefoxBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Mozilla Firefox\\firefox.exe"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
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
