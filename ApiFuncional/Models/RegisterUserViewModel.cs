using System.ComponentModel.DataAnnotations;

namespace ApiFuncional.Models;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [EmailAddress(ErrorMessage = "O campo {0} está com o formato invalido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [StringLength(maximumLength: 100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [Compare("Password", ErrorMessage = "As senhas não conferem!")]
    public string ConfirmPassword { get; set; }
}
