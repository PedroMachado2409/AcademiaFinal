using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mapping
{
    public class FichaDeTreinoProfile : Profile
    {
        public FichaDeTreinoProfile()
        {
            CreateMap<FichaDeTreino, FichaDeTreinoDTO>()
                .ReverseMap();

            CreateMap<ItemFichaDeTreino, ItemFichaDeTreinoDTO>()
            .ForMember(dest => dest.EquipamentoId, opt => opt.MapFrom(src => src.Equipamento.Id))
            .ForMember(dest => dest.EquipamentoNome, opt => opt.MapFrom(src => src.Equipamento.Nome))
            .ForMember(dest => dest.Repeticoes, opt => opt.MapFrom(src => src.Repeticoes))
            .ReverseMap()
            .ForPath(dest => dest.Equipamento, opt => opt.Ignore());
        }
    }
}
