using NexusGym.Application.Dto;
using NexusGym.Application.Dto.Planos;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mapping
{
    public class PlanoProfile : AutoMapper.Profile
    {
        public PlanoProfile()
        {
            CreateMap<Plano, PlanoResponseDTO>().ReverseMap();
            CreateMap<Plano, PlanoCreateDTO>().ReverseMap();
            CreateMap<Plano, PlanoUpdateDTO>().ReverseMap();
        }
    }
    
}
