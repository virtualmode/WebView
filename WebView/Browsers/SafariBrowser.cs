using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class SafariBrowser : Browser
{
    public SafariBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Safari.app"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
        }
    }

    public override void OpenApp(BrowserModes modes, string url, int left, int top, int width, int height)
    {
        OpenShell($"{url}");
    }
}
