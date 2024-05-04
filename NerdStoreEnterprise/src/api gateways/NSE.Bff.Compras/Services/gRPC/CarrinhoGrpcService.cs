using NSE.Bff.Compras.Models;
using NSE.Carrinho.API.Service.gRPC;

namespace NSE.Bff.Compras.Services.gRPC
{
    public class CarrinhoGrpcService : ICarrinhoGrpcService
    {
        private readonly CarrinhoCompras.CarrinhoComprasClient _client;

        public CarrinhoGrpcService(CarrinhoCompras.CarrinhoComprasClient client)
        {
            _client = client;
        }

        public async Task<CarrinhoDTO> ObterCarrinho()
        {
            var response = await _client.ObterCarrinhoAsync(new ObterCarrinhoRequest());
            return MapCarrinhoClienteProtoResponseToDTO(response);
        }

        private static CarrinhoDTO MapCarrinhoClienteProtoResponseToDTO(CarrinhoClienteResponse carrinhoResponse)
        {
            var carrinhoDTO = new CarrinhoDTO
            {
                ValorTotal = (decimal)carrinhoResponse.Valortotal,
                Desconto = (decimal)carrinhoResponse.Desconto,
                VoucherUtilizado = carrinhoResponse.Voucherutilizado
            };

            if (carrinhoResponse.Voucher != null)
            {
                carrinhoDTO.Voucher = new VoucherDTO
                {
                    Codigo = carrinhoResponse.Voucher.Codigo,
                    Percentual = (decimal?)carrinhoResponse.Voucher.Percentual,
                    ValorDesconto = (decimal?)carrinhoResponse.Voucher.Valordesconto,
                    TipoDesconto = carrinhoResponse.Voucher.Tipodesconto
                };
            }

            foreach (var item in carrinhoResponse.Items)
            {
                carrinhoDTO.Itens.Add(new ItemCarrinhoDTO
                {
                    Nome = item.Nome,
                    Imagem = item.Imagem,
                    ProdutoId = Guid.Parse(item.Produtoid),
                    Quantidade = item.Quantidade,
                    Valor = (decimal)item.Valor
                });
            }

            return carrinhoDTO;
        }
    }
}
