using FluentValidation;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace NexusGym.Application.Validators
{
    public class PlanoValidator : AbstractValidator<PlanoDTO>
    {
        public PlanoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage(ExceptionsMessage.NomeObrigatorio);
            RuleFor(x => x.Descricao).NotEmpty().WithMessage(ExceptionsMessage.DescricaoObrigatoria);
            RuleFor(x => x.Valor).NotEmpty().WithMessage(ExceptionsMessage.ValorObrigatorio);
            RuleFor(x => x.DuracaoMeses).NotEmpty().WithMessage(ExceptionsMessage.DuracaoObrigatoria);
        }
    }
}
