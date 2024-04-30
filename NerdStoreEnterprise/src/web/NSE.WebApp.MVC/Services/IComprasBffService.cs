using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IComprasBffService
{
    // Carrinho
    Task<CarrinhoViewModel> ObterCarrinho();
    Task<int> ObterQuantidadeCarrinho();
    Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel carrinho);
    Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel carrinho);
    Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    Task<ResponseResult> AplicarVoucherCarrinho(string voucher);

    // Pedido
    Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao);
    Task<PedidoViewModel> ObterUltimoPedido();
    Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId();
    PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco);
}