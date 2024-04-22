using Microsoft.OpenApi.Models;

namespace NSE.Carrinho.API.Configurations;

public static class SwaggerConfig
{
    public static WebApplicationBuilder AddSwaggerConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "NerdStore Enterprise Carrinho API",
                Description = "API NerdStore de Carrinho",
                Contact = new OpenApiContact() { Name = "Maxuel Jr.", Email = "maxueltstz@hotmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            });
        });
        return builder;
    }

    public static WebApplication UseSwaggerConfig(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return app;
    }
}
