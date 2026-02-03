using FluentValidation;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Exceptions.Clientes;

namespace NexusGym.Application.Validators.Cliente
{
    public class ClienteUpdaterValidator : AbstractValidator<ClienteUpdateDTO>
    {
        public ClienteUpdaterValidator()
        {
                RuleFor(c => c.Nome).NotEmpty().WithMessage(ClientesExceptions.Cliente_NomeObrigatorio);
                RuleFor(c => c.Telefone).NotEmpty().WithMessage(ClientesExceptions.Cliente_TelefoneObrigatório);
                RuleFor(c => c.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage(ClientesExceptions.Cliente_EmailObrigatorio)
                    .EmailAddress().WithMessage(ClientesExceptions.Cliente_EmailInvalido);

                RuleFor(c => c.Cpf)
                     .Cascade(CascadeMode.Stop)
                     .NotEmpty().WithMessage(ClientesExceptions.Cliente_CpfObrigatorio)
                     .Length(11).WithMessage(ClientesExceptions.Cliente_CpfInvalido);

        }
    }
}
