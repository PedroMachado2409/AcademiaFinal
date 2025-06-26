using FluentValidation;
using NexusGym.Application.Dto;
using NexusGym.Exceptions;

namespace NexusGym.Application.Validators
{
    public class PlanoClienteValidator : AbstractValidator<PlanoClienteDTO>
    {
        public PlanoClienteValidator()
        { 
            RuleFor(x => x.ClienteId).NotEmpty().WithMessage(ExceptionsMessage.ClienteObrigatório);
            RuleFor(x => x.PlanoId).NotEmpty().WithMessage(ExceptionsMessage.PlanoObrigatorio);
        }
    }
}
