using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponsePossuiErros(ResponseResult result)
    {
        if(result is not null && result.Errors.Mensagens.Any()){

            foreach(var mensagem in result.Errors.Mensagens)
            {
                ModelState.AddModelError(string.Empty, mensagem);
            }

            return true;
        }

        return false;
    }

    protected void AdicionarErroValidacao(string mensagem)
    {
        ModelState.AddModelError(string.Empty, mensagem);
    }

    protected bool OperacaoValida()
    {
        return ModelState.ErrorCount == 0;
    }
}
