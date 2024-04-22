using NSE.Carrinho.API.Data;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Carrinho.API.Configurations;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();
        builder.Services.AddScoped<CarrinhoContext>();
        return builder;
    }
}
