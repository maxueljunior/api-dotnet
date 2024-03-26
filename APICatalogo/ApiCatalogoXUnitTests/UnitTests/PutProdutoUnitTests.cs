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

public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public PutProdutoUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PutProduto_Return_OkResult()
    {
        var prodId = 1;

        var updateProdutoDto = new ProdutoDTO
        {
            ProdutoId = prodId,
            Nome = "Produto Att",
            Descricao = "Minha Desc",
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };

        var result = await _controller.Put(prodId, updateProdutoDto);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PutProduto_Return_BadRequest()
    {
        var prodId = 1000;

        var updateProdutoDto = new ProdutoDTO
        {
            ProdutoId = 14,
            Nome = "Produto Att",
            Descricao = "Minha Desc",
            ImagemUrl = "imagem1.jpg",
            CategoriaId = 2
        };

        var data = await _controller.Put(prodId, updateProdutoDto);

        data.Result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
    }

}
