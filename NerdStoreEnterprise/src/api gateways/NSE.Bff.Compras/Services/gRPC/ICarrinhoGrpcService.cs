using NSE.Bff.Compras.Models;

namespace NSE.Bff.Compras.Services.gRPC
{
    public interface ICarrinhoGrpcService
    {
        Task<CarrinhoDTO> ObterCarrinho();
    }
}