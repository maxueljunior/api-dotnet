using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoXUnitTests.UnitTests;

public class ProdutosUnitTestsController
{
    public IUnitOfWork repository;
    public IMapper mapper;

    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    public static string connectionString = "Server=localhost;DataBase=apicatalogodb;Uid=root;Pwd=maxuelt123";

    static ProdutosUnitTestsController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutosUnitTestsController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoDTOMappingProfile());
        });

        mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);
        repository = new UnitOfWork(context);
    }
}
