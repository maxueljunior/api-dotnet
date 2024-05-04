using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Services;

public interface ICatalogoService
{
    Task<ProdutoViewModel> ObterPorId(Guid id);
    Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null);
}

public interface ICatalogoServiceRefit
{
    [Get("/api/catalogo/produtos")]
    Task<IEnumerable<ProdutoViewModel>> ObterTodos();

    [Get("/api/catalogo/produtos/{id}")]
    Task<ProdutoViewModel> ObterPorId(Guid id);
}