using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WebView.Browsers;

internal abstract class Browser : IBrowser
{
    #region Fields

    protected bool _isInstalled = false;

    protected string _installedFileName = string.Empty;

    #endregion Fields

    #region Properties

    public bool IsInstalled => _isInstalled;

    public string InstalledFileName => _installedFileName;

    #endregion Properties

    #region Methods

    /// <summary>
    /// Used to check possible paths of installed browsers.
    /// </summary>
    /// <param name="possibleFileNames"></param>
    /// <returns></returns>
    protected bool Check(List<string> possibleFileNames)
    {
        foreach (string possibleFileName in possibleFileNames)
        {
            if (File.Exists(possibleFileName) ||
                Directory.Exists(possibleFileName)) // For MacOS apps.
            {
                _isInstalled = true;
                _installedFileName = possibleFileName;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Native command to obtain current screen resolution.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected bool GetResolution(out int width, out int height)
    {
        width = height = -1;
        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = false;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            const string CURRENT_HORIZONTAL_RESOLUTION = "CurrentHorizontalResolution=";
            const string CURRENT_VERTICAL_RESOLUTION = "CurrentVerticalResolution=";

            process.StartInfo.FileName = "wmic";
            process.StartInfo.Arguments = "path Win32_VideoController get CurrentHorizontalResolution,CurrentVerticalResolution /format:value";
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();

            int i = 0;
            // You can implement loop to obtain all monitors.
            i = output.IndexOf(CURRENT_HORIZONTAL_RESOLUTION, i) + CURRENT_HORIZONTAL_RESOLUTION.Length;
            width = int.Parse(output.Substring(i, output.IndexOfAny(new char[] { '\r', '\n' }, i) - i));
            i = output.IndexOf(CURRENT_VERTICAL_RESOLUTION, i) + CURRENT_VERTICAL_RESOLUTION.Length;
            height = int.Parse(output.Substring(i, output.IndexOfAny(new char[] { '\r', '\n' }, i) - i));
            return true;
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

        return false;
    }

    /// <summary>
    /// Returns the path of the current user's temporary folder.
    /// </summary>
    /// <returns></returns>
    protected string GetUserTempPath()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Path.GetTempPath();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Example: /var/folders/gx/l15rx4_j0gv1979xg1plw8b40000gn/T/
            return Path.GetTempPath();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
        }

        return Path.GetTempPath();
    }

    /// <summary>
    /// Native command to open program.
    /// </summary>
    protected bool Open(string arguments)
    {
        if (!_isInstalled)
            return false;

        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.EnableRaisingEvents = false;

        process.StartInfo.FileName = _installedFileName;
        process.StartInfo.Arguments = arguments;
        process.Start();

        return true;
    }

    /// <summary>
    /// Opens program via appropriate shell.
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    protected bool OpenShell(string arguments)
    {
        if (!_isInstalled)
            return false;

        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.EnableRaisingEvents = false;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = $"/c start {_installedFileName} {arguments}";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            process.StartInfo.FileName = _installedFileName;
            process.StartInfo.Arguments = arguments;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = $"-a {_installedFileName} {arguments}";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            process.StartInfo.FileName = _installedFileName;
            process.StartInfo.Arguments = arguments;
        }
        else
        {
            process.StartInfo.FileName = _installedFileName;
            process.StartInfo.Arguments = arguments;
        }

        process.Start();

        return true;
    }

    public abstract void OpenApp(BrowserModes modes, string url, int left, int top, int width, int height);

    #endregion Methods
}
