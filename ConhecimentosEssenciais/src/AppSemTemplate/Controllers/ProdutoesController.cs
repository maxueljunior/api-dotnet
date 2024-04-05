using AppSemTemplate.Data;
using AppSemTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppSemTemplate.Controllers;

public class ProdutoesController : Controller
{

    private readonly AppDbContext _context;

    public ProdutoesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.ToListAsync());
    }

    //Details
    public async Task<IActionResult> Details(int? id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return View(produto);
    }

    //Criar
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Nome,Imagem,Valor")] Produto produto)
    {

        if(ModelState.IsValid)
        {
            await _context.AddAsync(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(produto);
    }

    //Editar
    public async Task<IActionResult> Editar(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if(produto is null)
        {
            return NotFound();
        }


        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(int id, [Bind("Nome,Imagem,Valor")] Produto produto)
    {

        if (produto is null)
        {
            return NotFound();
        }

        var prod = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (ModelState.IsValid)
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(produto);
    }

    //Deletar
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
