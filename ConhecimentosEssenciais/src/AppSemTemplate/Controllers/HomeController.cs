using AppSemTemplate.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AppSemTemplate.Controllers;

public class HomeController : Controller
{

    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    //[ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IActionResult Index()
    {
        ViewData["Message"] = _localizer["Seja bem vindo!"];

        ViewData["Horario"] = DateTime.Now;

        if(Request.Cookies.TryGetValue("MeuCookie", out string? cookieValue))
        {
            ViewData["MeuCookie"] = cookieValue;
        }

        return View();
    }

    [Route("cookies")]
    public IActionResult Cookie()
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1),
        };

        Response.Cookies.Append("MeuCookie", "Dados do Cookie", cookieOptions);

        return View("Index");
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)});

        return LocalRedirect(returnUrl);
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Errors(int id)
    {
        var modelErro = new ErrorViewModel();

        if(id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate o nosso suporte";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem = "A página que está procurando não existe! <br/>Em caso de duvidas contate o nosso suporte";
            modelErro.Titulo = "Ops! Pagina não encontrada";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Você não tem permissão para fazer isto.";
            modelErro.Titulo = "Acesso Negado!";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(500);
        }

        return View("Error", modelErro);
    }
}
