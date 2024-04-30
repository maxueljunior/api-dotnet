using NSE.Pagamentos.API.Data.Repository;
using NSE.Pagamentos.API.Data;
using NSE.Pagamentos.API.Models;
using NSE.WebAPI.Core.Usuario;
using NSE.Pagamentos.API.Facade;
using NSE.Pagamentos.API.Services;

namespace NSE.Pagamentos.API.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        builder.Services.AddScoped<IPagamentoService, PagamentoService>();
        builder.Services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

        builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        builder.Services.AddScoped<PagamentosContext>();

        return builder;
    }
}
