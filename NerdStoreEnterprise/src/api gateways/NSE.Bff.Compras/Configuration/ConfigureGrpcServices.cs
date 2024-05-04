using NSE.Bff.Compras.Services.gRPC;
using NSE.Carrinho.API.Service.gRPC;

namespace NSE.Bff.Compras.Configuration
{
    public static class ConfigureGrpcServices
    {
        public static WebApplicationBuilder AddConfigureGrpcServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<GrpcServiceInterceptor>();
            builder.Services.AddScoped<ICarrinhoGrpcService, CarrinhoGrpcService>();

            builder.Services.AddGrpcClient<CarrinhoCompras.CarrinhoComprasClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["AppSettingsUrl:CarrinhoUrl"]);
            }).AddInterceptor<GrpcServiceInterceptor>();

            return builder;
        }
    }
}
