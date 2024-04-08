using AppSemTemplate.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppSemTemplate.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "O campo {0} é obrigatorio")]
    public string? Nome { get; set; }


    [Required(ErrorMessage = "O campo {0} é obrigatorio")]
    public string? Imagem { get; set; }

    [Moeda]
    [Required(ErrorMessage = "O campo {0} é obrigatorio")]
    public decimal Valor {  get; set; }
}
