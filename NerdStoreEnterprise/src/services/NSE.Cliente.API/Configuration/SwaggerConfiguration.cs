namespace NSE.Cliente.API.Configuration;

public static class SwaggerConfiguration
{
    public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
