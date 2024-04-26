using NSE.Bff.Compras.Extensions;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Bff.Compras.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        var appSettingsServiceSection = builder.Configuration.GetSection("AppSettingsUrl");

        var appSettingsSection = builder.Configuration.GetSection("AppSettings");

        builder.Services.Configure<AppServicesSettings>(appSettingsServiceSection);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Total", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            });
        });
        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseCors("Total");
        app.UseAuthConfiguration();

        app.MapControllers();
        return app;
    }
}
