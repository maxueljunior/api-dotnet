using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    //private readonly IRepository<Produto> _repository;


    [HttpGet("produtos/{id}")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosCategoria(int id)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaAsync(id);

        if (produtos is null) return NotFound();

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPagination([FromQuery] ProdutosParameters produtosParams)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParams);
        return ObterProdutos(produtos);
    }

    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFiltroPreco);
        return ObterProdutos(produtos);

    }

    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(IPagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.Count,
            produtos.PageSize,
            produtos.PageCount,
            produtos.TotalItemCount,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtos = await _uof.ProdutoRepository.GetAllAsync();

        if (produtos is null)
        {
            return NotFound();
        }

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoCriado = _uof.ProdutoRepository.Create(produto);
        await _uof.CommitAsync();

        var produtoDtoCriado = _mapper.Map<ProdutoDTO>(produtoCriado);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoDtoCriado.ProdutoId }, produtoDtoCriado);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
    {
        if (patchProdutoDto is null || id <= 0)
            return BadRequest();

        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound();

        var produtoUpdateDtoRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDto.ApplyTo(produtoUpdateDtoRequest, ModelState);

        if(!ModelState.IsValid || TryValidateModel(produtoUpdateDtoRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateDtoRequest, produto);

        _uof.ProdutoRepository.Update(produto);
        await _uof.CommitAsync();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
        {
            return BadRequest();
        }
        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        await _uof.CommitAsync();

        var produtoDtoAtt = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoDtoAtt);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        await _uof.CommitAsync();

        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDto);
    }
}