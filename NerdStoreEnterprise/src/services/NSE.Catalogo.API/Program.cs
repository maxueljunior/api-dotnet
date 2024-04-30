using NSE.Catalogo.API.Configuration;
using NSE.WebAPI.Core.Identidade;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

builder.AddApiConfiguration();
builder.AddJwtConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.AddSwaggerConfiguration();
builder.AddDependencyInjectionConfiguration();
builder.AddMessageBusConfiguration();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
