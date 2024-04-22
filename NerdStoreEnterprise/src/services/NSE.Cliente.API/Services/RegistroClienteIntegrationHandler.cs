﻿using EasyNetQ;
using FluentValidation.Results;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Cliente.API.Services;

public class RegistroClienteIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();

        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
    {
        var clienteCommand = new RegisterClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
        ValidationResult sucesso;

        using(var scope =_serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            sucesso = await mediator.EnviarComando(clienteCommand);
        }

        return new ResponseMessage(sucesso);
    }

    private void OnConnect(object? sender, EventArgs e)
    {
        SetResponder();
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
            await RegistrarCliente(request));

        _bus.AdvancedBus.Connected += OnConnect;
    }
}
