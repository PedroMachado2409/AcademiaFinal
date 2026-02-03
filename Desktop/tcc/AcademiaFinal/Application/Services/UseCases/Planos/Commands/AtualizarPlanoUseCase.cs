using AutoMapper;
using NexusGym.Application.Dto.Planos;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions;
using System.Drawing;

namespace NexusGym.Application.Services.UseCases.Planos.Commands
{
    public class AtualizarPlanoUseCase
    {
        private readonly IPlanoRepository _repository;
        private readonly IMapper _mapper;

        public AtualizarPlanoUseCase (IPlanoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlanoResponseDTO> AtualizarPlano(PlanoUpdateDTO dto)
        {
            var planoExistente = await _repository.ObterPlanoPorId(dto.Id);

            if (planoExistente == null)
                throw new Exception(NotFoundExceptions.PlanoNotFound);

            planoExistente?.Atualizar(dto.Nome,dto.Descricao, dto.Valor, dto.DuracaoMeses);
            await _repository.AtualizarPlano(planoExistente);
            return _mapper.Map<PlanoResponseDTO>(planoExistente);
        }
    }
}
