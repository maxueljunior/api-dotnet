using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NSE.MessageBus;

public static class DependecyInjectionExtensions
{
    public static WebApplicationBuilder AddDependencyInjectionMessageBus(this WebApplicationBuilder builder, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

        builder.Services.AddSingleton<IMessageBus>(new MessageBus(connection));

        return builder;
    }
}
