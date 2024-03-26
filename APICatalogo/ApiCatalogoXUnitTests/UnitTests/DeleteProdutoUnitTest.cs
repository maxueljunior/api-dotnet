using APICatalogo.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTests.UnitTests;

public class DeleteProdutoUnitTest : IClassFixture<ProdutosUnitTestsController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutoUnitTest(ProdutosUnitTestsController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task DeleteProduto_Response_ObjectResultOk()
    {
        var prodId = 1;

        var result = await _controller.Delete(prodId);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteProduto_Response_NotFound()
    {
        var prodId = 1000;

        var result = await _controller.Delete(prodId);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
}
