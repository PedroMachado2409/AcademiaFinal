using FluentValidation;
using NexusGym.Application.Dto;
using NexusGym.Exceptions;

namespace NexusGym.Application.Validators
{
    public class EquipamentoValidator : AbstractValidator<EquipamentoDTO>
    {

        public EquipamentoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage(ExceptionsMessage.NomeObrigatorio);
            RuleFor(x => x.Descricao).NotEmpty().WithMessage(ExceptionsMessage.DescricaoObrigatoria);
            RuleFor(x => x.Marca).NotEmpty().WithMessage(ExceptionsMessage.MarcaObrigatoria);
            RuleFor(x => x.Peso).GreaterThan(0).WithMessage(ExceptionsMessage.PesoObrigatorio);
        }

    }
}
