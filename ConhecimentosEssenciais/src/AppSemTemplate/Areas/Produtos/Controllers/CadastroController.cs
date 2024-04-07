using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Areas.Produtos.Controllers;

[Area("Produtos")]
[Route("cadastro-produtos")]
public class CadastroController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
