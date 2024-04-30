using Microsoft.Extensions.Configuration;
using NSE.MessageBus;
using NSE.Core.Utils;
using NSE.Pedidos.API.Services;

namespace NSE.Pedidos.API.Configuration;

public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddDependencyInjectionMessageBus(builder.Configuration.GetMessageQueueConnections("MessageBus"));
        builder.Services.AddHostedService<PedidoOrquestradorIntegrationHandler>();
        return builder;
    }
}
