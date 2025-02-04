using CodeStash.Application;
using CodeStash.Infrastructure;
using CodeStash.Infrastructure.Seeder;
using CodeStash.UI.Components;
using CodeStash.UI.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = builder.Configuration["CodeStash:ApiUrl"] ??
    throw new InvalidOperationException("ApiUrl is not configured");

builder.Services.AddHttpClient<ISnippetsHttpService, SnippetsHttpService>(client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddApplicationServices(builder.Configuration, builder.Host);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMudServices();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        await DbInitializer.InitializeAsync(serviceProvider);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
