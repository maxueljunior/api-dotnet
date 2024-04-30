using NSE.Catalogo.API.Services;
using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Catalogo.API.Configuration;

public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddDependencyInjectionMessageBus(builder.Configuration.GetMessageQueueConnections("MessageBus"));
        builder.Services.AddHostedService<CatalogoIntegrationHandler>();

        return builder;
    }
}
