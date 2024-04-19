using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Data;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Cliente.API.Configuration;

public static class ApiConfiguration
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ClientesContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Total", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        return builder;
    } 

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {

        }

        app.UseHttpsRedirection();
        app.UseCors("Total");
        app.UseAuthConfiguration();

        app.MapControllers();

        return app;
    }
}
