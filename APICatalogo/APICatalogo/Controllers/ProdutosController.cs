using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    //private readonly IRepository<Produto> _repository;
    public ProdutosController(IProdutoRepository produtoRepository)
    {
        //_repository = repository;
        _produtoRepository = produtoRepository;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);

        if (produtos is null) return NotFound();

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _produtoRepository.GetAll();

        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var produtoCriado = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoCriado.ProdutoId }, produtoCriado);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }
        
        var produtoAtualizado = _produtoRepository.Update(produto);

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        var produtoDeletado = _produtoRepository.Delete(produto);

        return Ok(produtoDeletado);
    }
}