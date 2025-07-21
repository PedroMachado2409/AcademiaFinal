using FluentValidation;
using NexusGym.Application.Dto;
using NexusGym.Exceptions;

namespace NexusGym.Application.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage(ExceptionsMessage.NomeObrigatorio);
            RuleFor(c => c.Telefone).NotEmpty().WithMessage(ExceptionsMessage.TelefoneObrigatorio);
            RuleFor(c => c.Email).NotEmpty().WithMessage(ExceptionsMessage.EmailObrigatorio);
        }
    }
}
