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

public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestsController>
{

    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetProdutoById_OKResult()
    {
        //Arrange
        var prodId = 1;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (xunit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        //Assert (fluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>()
                    .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetProdutoById_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (fluentAssertions)
        data.Result.Should().BeOfType<NotFoundObjectResult>()
                    .Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetProdutoById_BadRequest()
    {
        //Arrange
        var prodId = -1;

        //Act
        var data = await _controller.Get(prodId);

        //Assert (fluentAssertions)
        data.Result.Should().BeOfType<BadRequestObjectResult>()
                    .Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetProdutos_Return_ListOfProdutoDTO()
    {
        //Arrange

        //Act
        var data = await _controller.Get();

        //Assert (fluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>()
                    .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>()
                    .And.NotBeNull();
    }

    [Fact]
    public async Task GetProdutos_Return_BadRequestResult()
    {
        //Arrange

        //Act
        var data = await _controller.Get();

        //Assert (fluentAssertions)
        data.Result.Should().BeOfType<BadRequestResult>();
    }
}
