using Microsoft.AspNetCore.Mvc;

namespace AppSemTemplate.Areas.Vendas.Controllers;

[Area("Vendas")]
[Route("gestao-vendas")]
public class GestaoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
