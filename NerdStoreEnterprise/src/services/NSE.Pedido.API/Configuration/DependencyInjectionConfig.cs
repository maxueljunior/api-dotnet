using NSE.Core.Mediator;
using NSE.Pedidos.API.Application.Queries;
using NSE.Pedidos.Domain.Vouchers;
using NSE.Pedidos.Infra.Data;
using NSE.Pedidos.Infra.Data.Repository;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Pedidos.API.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
        builder.Services.AddScoped<IVoucherQueries, VoucherQueries>();

        builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
        builder.Services.AddScoped<PedidosContext>();
        return builder;
    }
}
