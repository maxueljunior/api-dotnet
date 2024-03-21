using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {

    }

    //public IEnumerable<Produto> GetProdutos(ProdutosParameters produtoParams)
    //{
    //    return GetAll()
    //        .OrderBy(on => on.Nome)
    //        .Skip((produtoParams.PageNumber - 1) * produtoParams.PageSize)
    //        .Take(produtoParams.PageSize)
    //        .ToList();
    //}

    public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtoParams)
    {
        var produtos = await GetAllAsync();

        var produtosOrd = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        //var produtosOrdenados = IPagedList<Produto>.ToPagedList(produtosOrd, produtoParams.PageNumber, produtoParams.PageSize); 

        var produtosOrdenados = await produtosOrd.ToPagedListAsync(produtoParams.PageNumber, produtoParams.PageSize);

        return produtosOrdenados;
    }

    public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPrecoParams)
    {
        var produtos = await GetAllAsync();

        if(produtosFiltroPrecoParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroPrecoParams.PrecoCriterio))
        {
            if(produtosFiltroPrecoParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase)){
                produtos = produtos.Where(p => p.Preco > produtosFiltroPrecoParams.Preco).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroPrecoParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase)){
                produtos = produtos.Where(p => p.Preco < produtosFiltroPrecoParams.Preco).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroPrecoParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase)){
                produtos = produtos.Where(p => p.Preco == produtosFiltroPrecoParams.Preco).OrderBy(p => p.Preco);
            }
        }

        //var produtosFiltrados = IPagedList<Produto>.ToPagedList(produtos.AsQueryable(), produtosFiltroPrecoParams.PageNumber, produtosFiltroPrecoParams.PageSize);
        var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroPrecoParams.PageNumber, produtosFiltroPrecoParams.PageSize);

        return produtosFiltrados;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();

        return produtos.Where(c => c.CategoriaId == id);
    }
}
