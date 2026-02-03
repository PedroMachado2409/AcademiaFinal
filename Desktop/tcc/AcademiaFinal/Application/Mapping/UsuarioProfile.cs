using AutoMapper;
using NexusGym.Application.Dto.Usuarios;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<UsuarioCreateDTO, Usuario>().ReverseMap();
        }
    }
}
