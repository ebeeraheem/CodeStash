using CodeStash.API;
using CodeStash.Application;
using CodeStash.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddApiServices(builder.Host);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("CodeStash");
});

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
