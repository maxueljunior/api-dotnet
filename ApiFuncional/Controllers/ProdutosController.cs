using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFuncional.Controllers;

[Authorize]
[Route("api/produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        if(_context.Produtos is null) return NotFound();

        return await _context.Produtos.ToListAsync();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        if (_context.Produtos is null) return NotFound();

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if(produto is null) return NotFound();

        return produto;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        if (_context.Produtos is null) return Problem("Erro ao criar produto, contate o suporte!");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();

        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        
        _context.Entry(produto).State = EntityState.Modified;

        try 
        { 
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> DeleteProduto(int id)
    {
        if (_context.Produtos is null) return NotFound();

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if(produto is null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProdutoExists(int id)
    {
        return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
