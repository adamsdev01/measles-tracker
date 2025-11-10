using measles_tracker.app;
using measles_tracker.app.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Register application services
builder.Services.AddScoped<CSVDataService>();
builder.Services.AddScoped<GithubRepoService>();
#endregion

await builder.Build().RunAsync();

