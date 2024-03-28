using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMvc.Controllers;

public class AccountController : Controller
{
    private readonly IAutenticacao _autenticaoService;

    public AccountController(IAutenticacao autenticaoService)
    {
        _autenticaoService = autenticaoService;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login invalido...");
            return View(model);
        }

        var result = await _autenticaoService.AutenticaUsuario(model);

        if(result is null)
        {
            ModelState.AddModelError(string.Empty, "Login invalido...");
            return View(model);
        }

        Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
        });

        return Redirect("/");
    }
}
