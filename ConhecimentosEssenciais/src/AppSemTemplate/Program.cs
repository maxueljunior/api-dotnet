using AppSemTemplate.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGlobalizationConfig()
    .AddMvcConfiguration()
    .AddDependencyInjection()
    .AddIdentityConfig();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
