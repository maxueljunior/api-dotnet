using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

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

    public PagedList<Produto> GetProdutos(ProdutosParameters produtoParams)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();

        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParams.PageNumber, produtoParams.PageSize); 

        return produtosOrdenados;
    }

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPrecoParams)
    {
        var produtos = GetAll().AsQueryable();

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

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroPrecoParams.PageNumber, produtosFiltroPrecoParams.PageSize);

        return produtosFiltrados;
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
