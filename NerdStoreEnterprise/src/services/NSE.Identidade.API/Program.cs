using NSE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.AddApiConfiguration()
       .AddIdentityConfig()
       .AddSwaggerConfig();

var app = builder.Build();

app.UseSwaggerConfig()
   .UseApiConfiguration();

app.Run();

