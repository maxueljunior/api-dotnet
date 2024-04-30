using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSE.Pagamentos.API.Data;
using NSE.Pagamentos.API.Facade;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Pagamentos.API.Configuration;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PagamentosContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllers();

        builder.Services.Configure<PagamentoConfig>(builder.Configuration.GetSection("PagamentoConfig"));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseCors("Total");
        app.UseAuthConfiguration();
        app.MapControllers();
        return app;
    }
}
