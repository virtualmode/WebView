using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class ChromeBrowser : Browser
{
    public ChromeBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google\\Chrome\\Application\\chrome.exe"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google Chrome.app/Contents/MacOS/Google Chrome"),
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
