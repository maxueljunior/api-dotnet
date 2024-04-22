using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSE.WebAPI.Core.Usuario;
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
        builder.Services.AddSingleton<IValidationAttributeAdapterProvider, ValidationAttributeAdapterProvider>();
        builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        #region HttpServices
        
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

        builder.Services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
            .AddPolicyHandler(retryWaitPolicy)
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        builder.Services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(retryWaitPolicy)
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        builder.Services.AddHttpClient<ICarrinhoService, CarrinhoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(retryWaitPolicy)
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        #endregion


        return builder;
    }
}
