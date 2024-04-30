﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Commands;
using NSE.Cliente.API.Models;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;

namespace NSE.Cliente.API.Controllers;

public class ClientesController : MainController
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IAspNetUser _user;

    public ClientesController(IClienteRepository clienteRepository, IMediatorHandler mediator, IAspNetUser user)
    {
        _clienteRepository = clienteRepository;
        _mediator = mediator;
        _user = user;
    }

    [HttpGet("cliente/endereco")]
    public async Task<IActionResult> ObterEndereco()
    {
        var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

        return endereco == null ? NotFound() : CustomResponse(endereco);
    }

    [HttpPost("cliente/endereco")]
    public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
    {
        endereco.ClienteId = _user.ObterUserId();
        return CustomResponse(await _mediator.EnviarComando(endereco));
    }
}
