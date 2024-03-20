using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }

    [Required]
    public string? Nome { get; set; }

    [Required]
    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    [Required]
    public string? ImagemUrl { get; set; }

    public int CategoriaId { get; set; }
}
