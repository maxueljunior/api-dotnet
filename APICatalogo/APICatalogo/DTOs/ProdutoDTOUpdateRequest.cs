using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    [Range(1,99999, ErrorMessage = "O Estoque deve estar entre 1 e 99999")]
    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(DataCadastro.Date <= DateTime.Now)
        {
            yield return new ValidationResult("A data deve ser maior que a data atual", new[] {nameof(this.DataCadastro)});
        }
    }
}
