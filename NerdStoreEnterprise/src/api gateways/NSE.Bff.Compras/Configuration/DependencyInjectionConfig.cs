using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services;
using NSE.WebAPI.Core.Extensions;
using NSE.WebAPI.Core.Usuario;
using Polly;

namespace NSE.Bff.Compras.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        #region HttpServices

        builder.Services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        builder.Services.AddHttpClient<ICarrinhoService, CarrinhoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        #endregion

        return builder;
    }
}
