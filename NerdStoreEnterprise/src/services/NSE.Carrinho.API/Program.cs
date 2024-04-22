using NSE.Carrinho.API.Configurations;
using NSE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

builder.AddApiConfiguration();
builder.AddJwtConfiguration();
builder.AddDependencyInjectionConfiguration();
builder.AddSwaggerConfig();

var app = builder.Build();

app.UseSwaggerConfig();
app.UseApiConfiguration();

app.Run();
 