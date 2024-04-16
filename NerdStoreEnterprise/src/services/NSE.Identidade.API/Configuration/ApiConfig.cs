﻿namespace NSE.Identidade.API.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        app.UseHttpsRedirection();

        app.UseIdentityConfig();

        app.MapControllers();

        return app;
    }
}
