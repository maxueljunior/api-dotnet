using DemoVsCode;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseMiddleware<MeuMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
