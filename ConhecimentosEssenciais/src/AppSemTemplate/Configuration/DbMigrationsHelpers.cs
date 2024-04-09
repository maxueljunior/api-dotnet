using AppSemTemplate.Data;
using AppSemTemplate.Models;
using Microsoft.AspNetCore.Identity;

namespace AppSemTemplate.Configuration;

public static class DbMigrationsHelpers
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if(env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.EnsureCreatedAsync();
            await EnsureSeedProducts(context);
        }
    }

    private static async Task EnsureSeedProducts(AppDbContext context)
    {
        if (context.Produtos.Any()) return;

        await context.Produtos.AddAsync(new Produto() { Nome = "Livro CSS", Imagem = "CSS.jpg", Valor= 50, Processado = false});
        await context.Produtos.AddAsync(new Produto() { Nome = "Livro JQuery", Imagem = "JQuery.jpg", Valor = 150, Processado = false });
        await context.Produtos.AddAsync(new Produto() { Nome = "Livro HTML", Imagem = "HTML.jpg", Valor = 90, Processado = false });
        await context.Produtos.AddAsync(new Produto() { Nome = "Livro Razor", Imagem = "Razor.jpg", Valor = 50, Processado = false });

        await context.SaveChangesAsync();

        await context.Users.AddAsync(new IdentityUser()
        {
            Id = "3adcd244-4e1c-4570-a68e-0a6474ab6870",
            UserName = "maxueltstz@hotmail.com",
            NormalizedUserName = "MAXUELTSTZ@HOTMAIL.COM",
            Email = "maxueltstz@hotmail.com",
            NormalizedEmail = "MAXUELTSTZ@HOTMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEJbvy2hrLa/ncV2+EICGVKNG10sEON9E000NWM2hD9Hv3rin8N4CxXIJaz5swgnUsg==",
            SecurityStamp = "3RKSCJO24E4GTX3UBTGMPUUNZJ2H7L4L",
            ConcurrencyStamp = "bd9665c6-9202-402c-8e65-7d994ca33ca5",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            AccessFailedCount = 0
        });

        await context.SaveChangesAsync();

        if (context.UserClaims.Any()) return;

        await context.UserClaims.AddAsync(new IdentityUserClaim<string>
        {
            UserId = "3adcd244-4e1c-4570-a68e-0a6474ab6870",
            ClaimType = "Produtos",
            ClaimValue = "VI,ED,AD,EX"
        });

        await context.SaveChangesAsync();
    }
}
