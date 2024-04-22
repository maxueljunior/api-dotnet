using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Identidade.API.Configuration;

public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddDependencyInjectionMessageBus(builder.Configuration.GetMessageQueueConnections("MessageBus"));

        return builder;
    }
}
