using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal class EdgeBrowser : Browser
{
    public EdgeBrowser()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft\\Edge\\Application\\msedge.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft\\Edge\\Application\\msedge.exe"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Check(new List<string>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Microsoft Edge.app/Contents/MacOS/Microsoft Edge"),
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
        }
    }

    public override void OpenApp(BrowserModes modes, string url, int left, int top, int width, int height)
    {
        // Center screen positioning.
        if (modes.HasFlag(BrowserModes.CenterScreen) && GetResolution(out int screenWidth, out int screenHeight))
        {
            left = screenWidth / 2 - width / 2;
            top = screenHeight / 2 - height / 2;
        }

        // User data argument is used to fix windows position and size behavior.
        Open($"--app=\"{url}\" --new-window --window-position={left},{top} --window-size={width},{height} --user-data-dir=\"{Path.Combine(GetUserTempPath(), "webview_user_data")}\"");
    }
}
