﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Compras.Models;
using NSE.Bff.Compras.Services;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Bff.Compras.Controllers;

[Authorize]
public class CarrinhoController : MainController
{
    private readonly ICarrinhoService _carrinhoService;
    private readonly ICatalogoService _catalogoService;
    private readonly IPedidoService _pedidoService;

    public CarrinhoController(ICarrinhoService carrinhoService, ICatalogoService catalogoService, IPedidoService pedidoService)
    {
        _carrinhoService = carrinhoService;
        _catalogoService = catalogoService;
        _pedidoService = pedidoService;
    }

    [HttpGet("compras/carrinho")]
    public async Task<IActionResult> Index()
    {
        return CustomResponse(await _carrinhoService.ObterCarrinho());
    }

    [HttpGet("compras/carrinho-quantidade")]
    public async Task<int> ObterQuantidadeCarrinho()
    {
        var quantidade = await _carrinhoService.ObterCarrinho();
        return quantidade?.Itens.Sum(i => i.Quantidade) ?? 0;
    }

    [HttpPost("compras/carrinho/items")]
    public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO itemProduto)
    {
        var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

        await ValidarItemCarrinho(produto, itemProduto.Quantidade);
        if (!OperacaoValida()) return CustomResponse();

        itemProduto.Nome = produto.Nome;
        itemProduto.Valor = produto.Valor;
        itemProduto.Imagem = produto.Imagem;

        var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

        return CustomResponse(resposta);
    }

    [HttpPut("compras/carrinho/items/{produtoId}")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO itemProduto)
    {
        var produto = await _catalogoService.ObterPorId(produtoId);

        await ValidarItemCarrinho(produto, itemProduto.Quantidade);
        if (!OperacaoValida()) return CustomResponse();

        var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

        return CustomResponse(resposta);
    }

    [HttpDelete("compras/carrinho/items/{produtoId}")]
    public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
    {
        var produto = await _catalogoService.ObterPorId(produtoId);

        if(produto is null)
        {
            AdicionaErroProcessamento("Produto inexistente");
            return CustomResponse();
        }

        var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

        return CustomResponse(resposta);
    }

    [HttpPost("compras/carrinho/aplicar-voucher")]
    public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
    {
        var voucher = await _pedidoService.ObterVoucherPorCodigo(voucherCodigo);
        if(voucher is null)
        {
            AdicionaErroProcessamento("Voucher inválido ou não encontrado!");
            return CustomResponse();
        }

        var resposta = await _carrinhoService.AplicarVoucherCarrinho(voucher);
        return CustomResponse(resposta);
    }

    private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidade)
    {
        if (produto == null) AdicionaErroProcessamento("Produto inexistente!");
        if (quantidade < 1) AdicionaErroProcessamento($"Escolhe ao menos uma unidade do produto {produto.Nome}");

        var carrinho = await _carrinhoService.ObterCarrinho();
        var itemCarrinho = carrinho.Itens.FirstOrDefault(p => p.ProdutoId == produto.Id);

        if(itemCarrinho != null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
        {
            AdicionaErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
            return;
        }

        if (quantidade > produto.QuantidadeEstoque) AdicionaErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
    }
}