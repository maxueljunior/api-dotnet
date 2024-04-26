using Microsoft.OpenApi.Models;

namespace NSE.Pedido.API.Configuration;

public static class SwaggerConfig
{
    public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(name: "v1", new OpenApiInfo
            {
                Title = "NerdStore Enterprise Pedidos API",
                Description = "API NerdStore de Pedidos",
                Contact = new OpenApiContact() { Name = "Maxuel Jr.", Email = "maxueltstz@hotmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            });
        });
        return builder;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return app;
    }
}
