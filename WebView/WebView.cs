using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace WebView;

public static class WebView
{
    public static void UseWebView(this IApplicationBuilder builder)
    {
        Assembly assembly = typeof(WebView).Assembly;
        builder.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new ManifestEmbeddedFileProvider(assembly, "wwwroot"),
            RequestPath = new PathString($"/_content/{assembly.GetName().Name}")
        });
    }
}
