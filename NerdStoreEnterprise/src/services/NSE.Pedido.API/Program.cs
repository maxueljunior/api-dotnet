using NSE.Pedido.API.Configuration;
using NSE.WebAPI.Core.Identidade;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

builder.AddApiConfiguration();
builder.AddJwtConfiguration();
builder.AddSwaggerConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();
