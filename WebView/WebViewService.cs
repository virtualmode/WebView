using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WebView.Browsers;
using WebView.Circuits;

namespace WebView;

public abstract class WebViewService : BackgroundService
{
    /// <summary>
    /// Run a WebView window in debug mode if no browser was open after.
    /// </summary>
    private const int DEBUG_DELAY_MILLISECONDS = 1500;

    /// <summary>
    /// Helps circuit to detect that browser has been closed all connections in this time.
    /// </summary>
    private const int WAIT_ANY_CONNECTION_MILLISECONDS = 3000;

    /// <summary>
    /// Supported browsers.
    /// </summary>
    private readonly List<IBrowser> _browsers;

    protected readonly ILogger<WebViewService> _logger;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly IHostApplicationLifetime _hostLifetime;
    protected readonly ICircuitService _circuitService;

    private bool _firstConnectionOccurred;

    /// <summary>
    /// Service assembly.
    /// </summary>
    public static Assembly Assembly { get; } = typeof(WebViewService).Assembly;

    /// <summary>
    /// Service assembly name.
    /// </summary>
    public static string Name { get; } = Assembly.GetName().Name ?? throw new Exception("Can't get assembly information.");

    public WebViewService(ILogger<WebViewService> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostLifetime, ICircuitService circuitService)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hostLifetime = hostLifetime;
        _circuitService = circuitService;

        _browsers = InitializeBrowsers();
        _firstConnectionOccurred = false;

        _circuitService.CircuitStateChanged += OnCircuitStateChanged;
    }

    /// <summary>
    /// Initialize browsers with priority for particular operating system.
    /// </summary>
    /// <returns></returns>
    private List<IBrowser> InitializeBrowsers()
    {
        /*if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
        }*/

        // Windows and other operating systems.
        return new List<IBrowser>
        {
            // Modern browsers with app mode and PWA support.
            new EdgeBrowser(),
            new ChromeBrowser(),
            new ChromiumBrowser(),
            // MacOS limited behavior.
            new OperaBrowser(),
            new YandexBrowser(),
            // Limited support browsers.
            new SafariBrowser(),
            new FirefoxBrowser(),
        };
    }

    private void OnCircuitStateChanged(object? sender, CircuitStateChangeEventArgs e)
    {
        if (!_firstConnectionOccurred && e.State == CircuitState.Added)
            _firstConnectionOccurred = true;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        // Waiting for all client windows to close.
        Task.Run(async () => {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(WAIT_ANY_CONNECTION_MILLISECONDS, cancellationToken);
                if (_firstConnectionOccurred && _circuitService.ClientCount <= 0)
                    _hostLifetime.StopApplication();
            }
        });

        return base.StartAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _logger.LogInformation($"{nameof(WebViewService)} is disposing.");

        base.Dispose();
    }

    private async Task CheckHostStart()
    {
        if (_firstConnectionOccurred)
            return;

        // Waiting for socket initialization.
        var startedSource = new TaskCompletionSource();
        _hostLifetime.ApplicationStarted.Register(() => startedSource.SetResult());
        await startedSource.Task.ConfigureAwait(false);
    }

    protected void OpenFirstOrNothing(BrowserModes modes, string url, int left, int top, int width, int height)
    {
        foreach (var browser in _browsers)
        {
            if (browser.IsInstalled)
            {
                browser.OpenApp(modes, url, left, top, width, height);
                break;
            }
        }
    }

    /// <summary>
    /// Opens main window in app mode.
    /// </summary>
    public async void OpenApp(BrowserModes modes, int left, int top, int width, int height)
    {
        await CheckHostStart();

        // Crutch implementation debugging with external browser.
        if (Debugger.IsAttached)
        {
            Thread.Sleep(DEBUG_DELAY_MILLISECONDS);
            if (_circuitService.ClientCount > 0)
                return;
        }

        // Detect listening addresses and open browser.
        var server = _serviceProvider.GetRequiredService<IServer>();
        var addressFeature = server.Features.Get<IServerAddressesFeature>();
        if (addressFeature != null)
        {
            OpenFirstOrNothing(modes, addressFeature.Addresses.FirstOrDefault() ?? "http://localhost:5000", left, top, width, height);
        }
    }
}
