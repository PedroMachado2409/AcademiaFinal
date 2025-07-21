using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;

namespace NexusGym.Application.Mapping
{
    public class EquipamentoProfile : Profile
    {
        public EquipamentoProfile() {
            CreateMap<Equipamento, EquipamentoDTO>().ReverseMap();
        }
    }
}
