using AppSemTemplate.Data;
using Microsoft.AspNetCore.Identity;

namespace AppSemTemplate.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultIdentity<IdentityUser>(o =>
        {
            o.SignIn.RequireConfirmedAccount = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthorization(o =>
        {
            o.AddPolicy("PodeExcluirPermanentemente", policy => policy.RequireRole("Admin"));
        });

        return builder;
    }
}
