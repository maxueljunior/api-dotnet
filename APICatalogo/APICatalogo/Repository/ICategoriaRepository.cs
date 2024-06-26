﻿using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParams);
    Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNomeParams);
}
