using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Application.Dto.Equipamentos;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mapping
{
    public class EquipamentoProfile : Profile
    {
        public EquipamentoProfile() {
            CreateMap<Equipamento, EquipamentoResponseDTO>().ReverseMap();
            CreateMap<Equipamento, EquipamentoCreateDTO>().ReverseMap();
            CreateMap<Equipamento, EquipamentoUpdateDTO>().ReverseMap();
        }
    }
}
