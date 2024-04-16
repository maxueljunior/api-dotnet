using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentityConfiguration();
builder.AddMvcConfiguration();
builder.AddDependencyInjectionConfig();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
