using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

//[Authorize]
public class CarrinhoController : MainController
{
    private readonly IComprasBffService _carrinhoService;

    public CarrinhoController(IComprasBffService carrinhoService)
    {
        _carrinhoService = carrinhoService;
    }

    [Route("carrinho")]
    public async Task<IActionResult> Index()
    {
        return View(await _carrinhoService.ObterCarrinho());
    }

    [HttpPost]
    [Route("carrinho/adicionar-item")]
    public async Task<IActionResult> AdicionarItemCarrinho(ItemProdutoViewModel itemProduto)
    {
        var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

        if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/atualizar-item")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
    {
        var itemProduto = new ItemProdutoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
        var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

        if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/remover-item")]
    public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
    {
        var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

        if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("carrinho/aplicar-voucher")]
    public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
    {
        var resposta = await _carrinhoService.AplicarVoucherCarrinho(voucherCodigo);

        if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

        return RedirectToAction("Index");
    }
}