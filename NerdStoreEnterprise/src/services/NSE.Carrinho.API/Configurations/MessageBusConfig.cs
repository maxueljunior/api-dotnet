using NSE.MessageBus;
using NSE.Core.Utils;
using NSE.Carrinho.API.Services;

namespace NSE.Carrinho.API.Configurations;


public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddDependencyInjectionMessageBus(builder.Configuration.GetMessageQueueConnections("MessageBus"));
        builder.Services.AddHostedService<CarrinhoIntegrationHandler>();
        return builder;
    }
}
