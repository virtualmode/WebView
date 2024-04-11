using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.FileProviders;

using Samples;
using Samples.Components;
using WebView;
using WebView.Circuits;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
// Add WebView services.
builder.Services.AddSingleton<ICircuitService, CircuitService>();
builder.Services.AddScoped<CircuitHandler>((serviceProvider) => new CircuitHandlerService(serviceProvider.GetRequiredService<ICircuitService>()));
// Main services.
builder.Services.AddHostedService<AppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Add static files sources.
app.UseStaticFiles(); // Default physical source to debug.
app.UseStaticFiles(new StaticFileOptions() // Current assembly embedded resources.
{
    FileProvider = new ManifestEmbeddedFileProvider(typeof(Program).Assembly, "wwwroot")
});
app.UseWebView(); // Razor class library embedded resources.

// Using provider composition.
/*app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new CompositeFileProvider(app.Environment.WebRootFileProvider,
        new ManifestEmbeddedFileProvider(typeof(Program).Assembly, "wwwroot"))
});*/

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
