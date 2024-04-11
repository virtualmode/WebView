using WebView;
using WebView.Browsers;
using WebView.Circuits;

namespace Samples;

public sealed class AppService : WebViewService
{
    public AppService(ILogger<WebViewService> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostLifetime, ICircuitService circuitService) :
        base(logger, serviceProvider, hostLifetime, circuitService)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        OpenApp(BrowserModes.Size | BrowserModes.CenterScreen, 100, 100, 500, 650);

        Console.WriteLine("Cookies: " + Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
        Console.WriteLine("Desktop: " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        Console.WriteLine("Favorites: " + Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
        Console.WriteLine("Fonts: " + Environment.GetFolderPath(Environment.SpecialFolder.Fonts));
        Console.WriteLine("History: " + Environment.GetFolderPath(Environment.SpecialFolder.History));
        Console.WriteLine("Personal: " + Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        Console.WriteLine("Programs: " + Environment.GetFolderPath(Environment.SpecialFolder.Programs));
        Console.WriteLine("Recent: " + Environment.GetFolderPath(Environment.SpecialFolder.Recent));
        Console.WriteLine("Resources: " + Environment.GetFolderPath(Environment.SpecialFolder.Resources));
        Console.WriteLine("Startup: " + Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        Console.WriteLine("System: " + Environment.GetFolderPath(Environment.SpecialFolder.System));
        Console.WriteLine("Templates: " + Environment.GetFolderPath(Environment.SpecialFolder.Templates));
        Console.WriteLine("Windows: " + Environment.GetFolderPath(Environment.SpecialFolder.Windows));
        Console.WriteLine("AdminTools: " + Environment.GetFolderPath(Environment.SpecialFolder.AdminTools));
        Console.WriteLine("ApplicationData: " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        Console.WriteLine("CommonDocuments: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
        Console.WriteLine("CommonMusic: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic));
        Console.WriteLine("CommonPictures: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures));
        Console.WriteLine("CommonPrograms: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms));
        Console.WriteLine("CommonStartup: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup));
        Console.WriteLine("CommonTemplates: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates));
        Console.WriteLine("CommonVideos: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos));
        Console.WriteLine("DesktopDirectory: " + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        Console.WriteLine("InternetCache: " + Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
        Console.WriteLine("LocalizedResources: " + Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources));
        Console.WriteLine("MyComputer: " + Environment.GetFolderPath(Environment.SpecialFolder.MyComputer));
        Console.WriteLine("MyDocuments: " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        Console.WriteLine("MyMusic: " + Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
        Console.WriteLine("MyPictures: " + Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        Console.WriteLine("MyVideos: " + Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
        Console.WriteLine("NetworkShortcuts: " + Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts));
        Console.WriteLine("PrinterShortcuts: " + Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts));
        Console.WriteLine("ProgramFiles: " + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        Console.WriteLine("SendTo: " + Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
        Console.WriteLine("StartMenu: " + Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
        Console.WriteLine("SystemX86: " + Environment.GetFolderPath(Environment.SpecialFolder.SystemX86));
        Console.WriteLine("UserProfile: " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        Console.WriteLine("CDBurning: " + Environment.GetFolderPath(Environment.SpecialFolder.CDBurning));
        Console.WriteLine("CommonAdminTools: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools));
        Console.WriteLine("CommonApplicationData: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
        Console.WriteLine("CommonDesktopDirectory: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));
        Console.WriteLine("CommonOemLinks: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonOemLinks));
        Console.WriteLine("CommonProgramFiles: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
        Console.WriteLine("CommonStartMenu: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu));
        Console.WriteLine("LocalApplicationData: " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        Console.WriteLine("ProgramFilesX86: " + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
        Console.WriteLine("CommonProgramFilesX86: " + Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86));

        // Periodical tasks.
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(3000, stoppingToken);
        }
    }
}
