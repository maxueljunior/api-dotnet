using NSE.Catalogo.API.Configuration;
using NSE.WebAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

builder.AddApiConfiguration()
       .AddJwtConfiguration()
       .AddSwaggerConfiguration()
       .AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
