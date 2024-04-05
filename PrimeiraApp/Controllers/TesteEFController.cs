using Microsoft.AspNetCore.Mvc;
using PrimeiraApp.Data;
using PrimeiraApp.Models;

namespace PrimeiraApp.Controllers;

public class TesteEFController : Controller
{
    private readonly AppDbContext _context;

    public TesteEFController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var aluno = new Aluno
        {
            Nome = "Maxuel",
            Email = "maxueltstz@hotmail.com",
            DataNascimento = DateTime.Now,
            Avaliacao = 5,
            Ativo = true
        };

        _context.Alunos.Add(aluno);
        _context.SaveChanges();

        var alunoChange = _context.Alunos.Where(a => a.Nome == "Maxuel").FirstOrDefault();

        return View();
    }
}
