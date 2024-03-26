using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTests.UnitTests;

public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;
    public PostProdutoUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PostProduto_Return_CreatedStatusCode()
    {
        var novoProduto = new ProdutoDTO
        {
            Nome = "Nome",
            Descricao = "Descricao",
            Preco = 10.99m,
            ImagemUrl = "imagemfake1.jpg",
            CategoriaId = 2
        };

        var data = await _controller.Post(novoProduto);

        var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
        createdResult.Subject.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task PostProduto_Return_BadRequest()
    {
        ProdutoDTO novoProduto = null;

        var data = await _controller.Post(novoProduto);

        var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
        badRequestResult.Subject.StatusCode.Should().Be(400);
    }
}
