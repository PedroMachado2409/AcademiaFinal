using AutoMapper;
using NexusGym.Application.Dto.Cliente;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mapping
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteResponseDTO>().ReverseMap(); 
            CreateMap<Cliente, ClienteCreateDTO>().ReverseMap();
            CreateMap<Cliente, ClienteUpdateDTO>().ReverseMap();

        }
    }
}
