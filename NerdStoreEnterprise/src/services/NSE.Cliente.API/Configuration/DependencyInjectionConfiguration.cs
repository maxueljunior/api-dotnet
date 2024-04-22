using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Commands;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Data;
using NSE.Cliente.API.Data.Repository;
using NSE.Cliente.API.Models;
using NSE.Cliente.API.Services;
using NSE.Core.Mediator;

namespace NSE.Cliente.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
        builder.Services.AddScoped<IRequestHandler<RegisterClienteCommand, ValidationResult>, ClienteCommandHandler>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<ClientesContext>();
        builder.Services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

        //builder.Services.AddHostedService<RegistroClienteIntegrationHandler>();

        return builder;
    }
}
