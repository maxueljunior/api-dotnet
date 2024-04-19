using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Cliente.API.Controllers;

public class ClientesController : MainController
{
    private readonly IMediatorHandler _mediator;

    public ClientesController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> Index()
    {
        var resultado = await _mediator.EnviarComando(
                new RegisterClienteCommand(
                    Guid.NewGuid(),
                    "Maxuel Jr.",
                    "maxueltstz@hotmail.com",
                    "46856949880"));

        return CustomResponse(resultado);
    }
}
