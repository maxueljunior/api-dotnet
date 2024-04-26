﻿using NSE.WebAPI.Core.Usuario;
using System.Net.Http.Headers;

namespace NSE.Bff.Compras.Extensions;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IAspNetUser _user;

    public HttpClientAuthorizationDelegatingHandler(IAspNetUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
        }

        var token = _user.ObterUserToken();

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
