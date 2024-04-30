using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Cliente.API.Application.Commands;

public class RegisterClienteCommand : Command
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegisterClienteCommand(Guid id, string nome, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }

    public override bool EhValido()
    {
        ValidationResult = new RegistrarClienteValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegistrarClienteValidation : AbstractValidator<RegisterClienteCommand>
{
    public RegistrarClienteValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do Cliente invalido!");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente não foi informado!");

        RuleFor(c => c.Cpf)
            .Must(TerCpfValido)
            .WithMessage("O CPF informado não é valido!");

        RuleFor(c => c.Email)
            .Must(TerEmailValido)
            .WithMessage("O email informado não é valido!");
    }

    protected static bool TerCpfValido(string cpf)
    {
        return Core.DomainObjects.Cpf.Validar(cpf);
    }

    protected static bool TerEmailValido(string email)
    {
        return Core.DomainObjects.Email.Validar(email);
    }
}
