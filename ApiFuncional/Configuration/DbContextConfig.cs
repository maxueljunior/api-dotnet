using ApiFuncional.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Configuration;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new ArgumentNullException();


        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


        return builder;
    }
}
