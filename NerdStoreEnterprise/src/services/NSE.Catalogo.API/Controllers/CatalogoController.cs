using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Catalogo.API.Controllers;

[Authorize]
[Route("api/catalogo")]
public class CatalogoController : MainController
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [AllowAnonymous]
    [HttpGet("produtos")]
    public async Task<IEnumerable<Produto>> Index()
    {
        return await _produtoRepository.ObterTodos();
    }

    [ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("produtos/{id:guid}")]
    public async Task<Produto> ProdutoDetalhe(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}
