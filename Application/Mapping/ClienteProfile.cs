using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain;

namespace NexusGym.Application.Mapping
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
