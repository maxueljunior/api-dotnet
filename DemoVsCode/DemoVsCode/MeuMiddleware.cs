using System.Diagnostics;
using Serilog;

namespace DemoVsCode;

public class MeuMiddleware
{
    private readonly RequestDelegate _next;

    public MeuMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var sw = Stopwatch.StartNew();

        await _next(httpContext);

        sw.Stop();

        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        Log.Information($"A execução demorou {sw.Elapsed.TotalMilliseconds}ms ({sw.Elapsed.Seconds} segundos)");
    }


}
