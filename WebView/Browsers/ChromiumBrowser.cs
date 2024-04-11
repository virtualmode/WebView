using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class ChromiumBrowser : Browser
{
    public ChromiumBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Check(new List<string>
            {
                "/usr/bin/chromium",
            });
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
