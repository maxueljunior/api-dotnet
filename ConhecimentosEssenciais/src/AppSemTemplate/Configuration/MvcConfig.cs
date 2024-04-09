using AppSemTemplate.Data;
using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace AppSemTemplate.Configuration;

public static class MvcConfig
{
    public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetCallingAssembly());

        //builder.Services.AddResponseCaching();

        builder.Services.AddControllersWithViews(o =>
        {
            o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            o.Filters.Add(typeof(FiltroAuditoria));
        })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"/var/data_protection_keys/"))
            .SetApplicationName("MinhaAppMVC");

        builder.Services.Configure<CookiePolicyOptions>(o =>
        {
            o.CheckConsentNeeded = context => true;
            o.MinimumSameSitePolicy = SameSiteMode.None;
            o.ConsentCookieValue = "true";
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(o =>
            o.UseSqlServer(connectionString));

        builder.Services.AddHostedService<ImageWatermarkService>();

        return builder;
    }

    public static WebApplication UseMvcConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();
        }
        //app.UseResponseCaching();
        app.UseGlobalizationConfig();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        DbMigrationsHelpers.EnsureSeedData(app).Wait();

        return app;
    }
}
