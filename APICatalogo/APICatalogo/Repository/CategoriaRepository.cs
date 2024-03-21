using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParams)
    {
        var categorias = await GetAllAsync();

        var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();

        //var categoriaOrdenadasResult = IPagedList<Categoria>.ToPagedList(categoriasOrdenadas, categoriasParams.PageNumber, categoriasParams.PageSize);
        var categoriaOrdenadasResult = await categoriasOrdenadas.ToPagedListAsync(categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriaOrdenadasResult;
    }

    public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNomeParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasFiltroNomeParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasFiltroNomeParams.Nome));
        }

        //var categoriasOrdenadas = IPagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriasFiltroNomeParams.PageNumber, categoriasFiltroNomeParams.PageSize);
        var categoriasOrdenadas = await categorias.ToPagedListAsync(categoriasFiltroNomeParams.PageNumber, categoriasFiltroNomeParams.PageSize);

        return categoriasOrdenadas;
    }
}
