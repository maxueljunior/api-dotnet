using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjectionConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

        return builder;
    }
}
