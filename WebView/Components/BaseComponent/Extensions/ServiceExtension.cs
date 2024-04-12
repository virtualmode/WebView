using System.Runtime.Serialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using WebView.Circuits;

namespace WebView;

public static class ServiceExtension
{
    public static void AddWebView(this IServiceCollection services)
    {
        services.AddSingleton<ICircuitService, CircuitService>();
        services.AddScoped<CircuitHandler>((serviceProvider) => new CircuitHandlerService(serviceProvider.GetRequiredService<ICircuitService>()));

        services.AddScoped<ObjectIDGenerator>();
        services.AddScoped<IComponentStyle, ComponentStyle>();
        services.AddScoped<ThemeProvider>();
        services.AddScoped<ScopedStatics>();
        services.AddScoped<LayerHostService>();
    }

    public static void UseWebViewStaticFiles(this IApplicationBuilder builder)
    {
        builder.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new ManifestEmbeddedFileProvider(WebViewService.Assembly, "wwwroot"),
            RequestPath = new PathString($"/_content/{WebViewService.Name}")
        });
    }
}
