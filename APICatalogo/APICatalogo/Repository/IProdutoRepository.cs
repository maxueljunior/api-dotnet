using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);

    //IEnumerable<Produto> GetProdutos(ProdutosParameters produtoParams);
    Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtoParams);
    Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPrecoParams);
}
