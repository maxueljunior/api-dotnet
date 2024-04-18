using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.Services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

        var retryWaitPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }, (outcome, timespan, retryCount, context) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Tentando pela {retryCount} vez!");
                Console.ForegroundColor = ConsoleColor.White;
            });

        builder.Services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(retryWaitPolicy)
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

        // --- REFIT Implementation...

        //var catalogoUrl = builder.Configuration.GetSection("AppSettings:CatalogoUrl");

        //builder.Services.AddHttpClient("Refit", options =>
        //    {
        //        options.BaseAddress = new Uri(catalogoUrl.Value);
        //    }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //      .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

        // ----


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IUser, AspNetUser>();

        return builder;
    }
}
