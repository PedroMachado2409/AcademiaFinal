using NexusGym.Application.Dto;
using NexusGym.Domain;

namespace NexusGym.Application.Mapping
{
    public class PlanoProfile : AutoMapper.Profile
    {
        public PlanoProfile()
        {
            CreateMap<Plano, PlanoDTO>().ReverseMap();
        }
    }
    
}
