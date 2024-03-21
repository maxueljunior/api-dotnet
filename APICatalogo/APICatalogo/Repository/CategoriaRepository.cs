using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {

    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams)
    {
        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();

        var categoriaOrdenadas = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriaOrdenadas;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNomeParams)
    {
        var categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasFiltroNomeParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasFiltroNomeParams.Nome));
        }

        var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, categoriasFiltroNomeParams.PageNumber, categoriasFiltroNomeParams.PageSize);

        return categoriasOrdenadas;
    }
}
