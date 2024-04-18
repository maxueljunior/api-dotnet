using NSE.Catalogo.API.Data.Repository;
using NSE.Catalogo.API.Data;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped<CatalogoContext>();

        return builder;
    }
}
