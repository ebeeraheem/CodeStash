using CodeStash.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Console.WriteLine("This is the base url: " + "apiBaseUrl");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
ArgumentNullException.ThrowIfNull(apiBaseUrl, nameof(apiBaseUrl));

Console.WriteLine("This is the base url: " + apiBaseUrl);

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(apiBaseUrl)
    });

await builder.Build().RunAsync();
