namespace WebView.Browsers;

internal interface IBrowser
{
    bool IsInstalled { get; }

    string InstalledFileName { get; }

    abstract void OpenApp(BrowserModes modes, string url, int left, int top, int width, int height);
}
