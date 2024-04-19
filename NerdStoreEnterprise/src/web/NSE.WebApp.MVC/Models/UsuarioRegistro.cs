using NSE.WebApp.MVC.Extensions;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models;

public class UsuarioRegistro
{
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [Display(Name = "Nome Completo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [Display(Name = "CPF")]
    [Cpf]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato invalido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Senha { get; set; }

    [Compare("Senha", ErrorMessage = "As senhas não conferem!")]
    [Display(Name = "Confirme a senha")]
    public string SenhaConfirmacao { get; set; }
}
