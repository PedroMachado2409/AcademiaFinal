using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Domain.Entities;
using NexusGym.Exceptions;
using NexusGym.Infrastructure.Repositories;

namespace NexusGym.Application.Services.NovaPasta
{
    public class FichaDeTreinoService
    {
        private readonly FichaDeTreinoRepository _fichaRepository;
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _mapper;

        public FichaDeTreinoService(
            FichaDeTreinoRepository fichaRepository,
            IEquipamentoRepository equipamentoRepository,
            IMapper mapper)
        {
            _fichaRepository = fichaRepository;
            _equipamentoRepository = equipamentoRepository;
            _mapper = mapper;
        }

        public async Task<List<FichaDeTreinoDTO>> ListarFichas()
        {
            var fichas = await _fichaRepository.ListarFichas();
            return _mapper.Map<List<FichaDeTreinoDTO>>(fichas);
        }

        public async Task<FichaDeTreinoDTO> ObterPorId(int id)
        {
            var ficha = await _fichaRepository.ObterPorId(id);
            if (ficha == null)
                throw new(NotFoundExceptions.FichaDeTreinoNotFound);

            return _mapper.Map<FichaDeTreinoDTO>(ficha);
        }

        public async Task<FichaDeTreinoDTO> CriarFicha(FichaDeTreinoDTO dto)
        {
            var ficha = _mapper.Map<FichaDeTreino>(dto);
            ficha.Itens.Clear();

            foreach (var itemDto in dto.Itens)
            {
                var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(itemDto.EquipamentoId);
                if (equipamento == null)
                    throw new(NotFoundExceptions.EquipamentoNotFound);

                ficha.Itens.Add(new ItemFichaDeTreino
                {
                    Equipamento = equipamento,
                    Repeticoes = itemDto.Repeticoes
                });
            }

            await _fichaRepository.AdicionarFicha(ficha);
            return _mapper.Map<FichaDeTreinoDTO>(ficha);
        }

        public async Task<FichaDeTreinoDTO> AtualizarFicha(FichaDeTreinoDTO dto)
        {
            var fichaExistente = await _fichaRepository.ObterPorId(dto.Id);
            if (fichaExistente == null)
                throw new(NotFoundExceptions.FichaDeTreinoNotFound);

            fichaExistente.ClienteId = dto.ClienteId;
            fichaExistente.UsuarioId = dto.UsuarioId;
            fichaExistente.Itens.Clear();

            foreach (var itemDto in dto.Itens)
            {
                var equipamento = await _equipamentoRepository.ObterEquipamentoPorId(itemDto.EquipamentoId);
                if (equipamento == null)
                    throw new(NotFoundExceptions.EquipamentoNotFound);

                fichaExistente.Itens.Add(new ItemFichaDeTreino
                {
                    Equipamento = equipamento,
                    Repeticoes = itemDto.Repeticoes
                });
            }

            await _fichaRepository.AtualizarFicha(fichaExistente);
            return _mapper.Map<FichaDeTreinoDTO>(fichaExistente);
        }

        public async Task<FichaDeTreinoDTO> Ativar(int id)
        {
            var ficha = await _fichaRepository.ObterPorId(id);
            if (ficha == null)
                throw new(NotFoundExceptions.FichaDeTreinoNotFound);
            ficha.Ativar();
            await _fichaRepository.AtualizarFicha(ficha);
            return _mapper.Map<FichaDeTreinoDTO>(ficha);
        }

        public async Task<FichaDeTreinoDTO> Desativar(int id)
        {
            var ficha = await _fichaRepository.ObterPorId(id);
            if (ficha == null)
                throw new(NotFoundExceptions.FichaDeTreinoNotFound);
            ficha.Inativar();
            await _fichaRepository.AtualizarFicha(ficha);
            return _mapper.Map<FichaDeTreinoDTO>(ficha);
        }
    }
}
