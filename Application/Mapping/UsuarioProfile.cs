using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;

namespace NexusGym.Application.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTO, Usuario>()
              .ReverseMap();
        }
    }
}
