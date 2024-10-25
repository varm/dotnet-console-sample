using dotnet_console_sample.DataAccess;
using dotnet_console_sample.Implement;
using dotnet_console_sample.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateDefaultBuilder(args);

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
.Build();

builder.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.ConfigureAppConfiguration((context, config) =>
{
    config.AddConfiguration(configuration);
});

Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateLogger();

builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<GenerDbContext>(options =>
    {
        options.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
    });
    services.AddSingleton<ISchool, School>();
    services.AddSingleton<ISclass, Sclass>();
});

builder.UseSerilog(Log.Logger);

Log.Information("Start...");

Console.WriteLine($"⚙ Current environment: {configuration["MySettings:Env"]}");

//Configuration & Logging test
var host = builder.Build();
var sclass = host.Services.GetRequiredService<ISclass>();
var result = await sclass.DisplayClassName();
Log.Information(result);

// IConfiguration _configuration = host.Services.GetRequiredService<IConfiguration>();
// var env = _configuration["MySettings:Env"];
// Console.WriteLine("Env: " + env);

// Data connection test
await sclass.GetCustomerList();

Log.Information("Stop...");
Log.CloseAndFlush();
Console.ReadLine();