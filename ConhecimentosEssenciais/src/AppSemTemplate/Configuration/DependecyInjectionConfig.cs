using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace AppSemTemplate.Configuration;

public static class DependecyInjectionConfig
{
    public static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IOperacao, Operacao>();
        builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
        return builder;
    }
}
