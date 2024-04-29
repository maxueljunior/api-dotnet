using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IComprasBffService
{
    Task<CarrinhoViewModel> ObterCarrinho();
    Task<int> ObterQuantidadeCarrinho();
    Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel produto);
    Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel carrinho);
    Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    Task<ResponseResult> AplicarVoucherCarrinho(string voucher);
}
