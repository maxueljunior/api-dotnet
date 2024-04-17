﻿using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AutenticacaoService : Services, IAutenticacaoService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(_appSettings!.AutenticacaoUrl!);
        _httpClient = httpClient;

        _appSettings = appSettings.Value;
    }

    public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
    {
        var loginContent = ObterConteudo(usuarioLogin);

        var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLogin
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }
        return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }

    public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
    {
        var registroContent = ObterConteudo(usuarioRegistro);

        var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

        if (!TratarErrosResponse(response))
        {
            return new UsuarioRespostaLogin
            {
                ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
            };
        }

        return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
    }
}
