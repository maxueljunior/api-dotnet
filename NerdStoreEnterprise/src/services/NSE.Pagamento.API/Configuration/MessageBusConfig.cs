using NSE.Core.Utils;
using NSE.MessageBus;
using NSE.Pagamentos.API.Services;

namespace NSE.Pagamentos.API.Configuration;

public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddDependencyInjectionMessageBus(builder.Configuration.GetMessageQueueConnections("MessageBus"));
        builder.Services.AddHostedService<PagamentoIntegrationHandler>();

        return builder;
    }
}
