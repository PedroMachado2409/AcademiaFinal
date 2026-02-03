using FluentValidation;
using NexusGym.Application.Dto;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Exceptions;
using NexusGym.Exceptions.Equipamentos;

namespace NexusGym.Application.Validators.Equipamento
{
    public class EquipamentoCreateValidator : AbstractValidator<EquipamentoCreateDTO>
    {

        public EquipamentoCreateValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage(EquipamentoExceptions.Equipamento_NomeObrigatorio);
            RuleFor(x => x.Marca).NotEmpty().WithMessage(EquipamentoExceptions.Equipamento_MarcaObrigatorio);
            RuleFor(x => x.Peso).GreaterThan(0).WithMessage(EquipamentoExceptions.Equipamento_PesoObrigatorio);
        }

    }
}
