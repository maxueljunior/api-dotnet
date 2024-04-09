using AppSemTemplate.Controllers;
using AppSemTemplate.Data;
using AppSemTemplate.Models;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace Testes;

public class ControllerTests
{
    [Fact]
    public void TestController_Index_Success()
    {
        //Arrange
        var controller = new TesteController();

        //Act
        var result = controller.Index();

        //Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void ProdutoController_Index_Sucesso()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var ctx = new AppDbContext(options);

        ctx.Produtos.Add(new Produto { Id = 1, Nome = "Produto 1", Valor = 10m});
        ctx.Produtos.Add(new Produto { Id = 2, Nome = "Produto 2", Valor = 20m });
        ctx.Produtos.Add(new Produto { Id = 3, Nome = "Produto 3", Valor = 30m });
        ctx.SaveChanges();

        var mockClaimsIdentity = new Mock<ClaimsIdentity>();
        mockClaimsIdentity.Setup(m => m.Name).Returns("teste@teste.com");

        var principal = new ClaimsPrincipal(mockClaimsIdentity.Object);

        var mockContext = new Mock<HttpContext>();
        mockContext.Setup(m => m.User).Returns(principal);

        var imageUploadService = new Mock<IImageUploadService>();

        var controller = new ProdutosController(ctx, imageUploadService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = mockContext.Object 
            }
        };

        //Act
        var result = controller.Index().Result;

        //Assert
        Assert.IsType<ViewResult>(result);

    }

    [Fact]
    public void ProdutoController_Create_Sucesso()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var ctx = new AppDbContext(options);
        

        var fileMock = new Mock<IFormFile>();
        var fileName = "test.jpg";
        fileMock.Setup(_ => _.FileName).Returns(fileName);

        var imageUploadService = new Mock<IImageUploadService>();
        imageUploadService.Setup(i => 
            i.UploadArquivo(
                new ModelStateDictionary(),
                fileMock.Object,
                It.IsAny<string>())
            ).ReturnsAsync(true);

        var produto = new Produto
        {
            Id = 1,
            ImagemUpload = fileMock.Object,
            Nome = "Teste",
            Valor = 50
        };

        var controller = new ProdutosController(ctx, imageUploadService.Object);

        //Act
        var result = controller.Create(produto).Result;

        //Assert
        Assert.IsType<RedirectToActionResult>(result);

    }

    [Fact]
    public void ProdutoController_Create_ValidationError()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var ctx = new AppDbContext(options);
        var fileMock = new Mock<IFormFile>();
        var imageUploadService = new Mock<IImageUploadService>();

        var produto = new Produto
        {
        };

        var controller = new ProdutosController(ctx, imageUploadService.Object);

        controller.ModelState.AddModelError("Nome", "O campo nome é requerido!");
        //Act
        var result = controller.Create(produto).Result;

        //Assert
        Assert.False(controller.ModelState.IsValid);
        Assert.IsType<ViewResult>(result);

    }
}