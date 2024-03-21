using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams);
    PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNomeParams);
}
