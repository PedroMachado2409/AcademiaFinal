using AutoMapper;
using NexusGym.Application.Dto;
using NexusGym.Domain.Entities;

namespace NexusGym.Application.Mappings
{
    public class PlanoClienteProfile : Profile
    {
        public PlanoClienteProfile()
        {
          
            CreateMap<PlanoCliente, PlanoClienteDTO>()
                .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome))
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.Plano.Nome));

            
            CreateMap<PlanoClienteDTO, PlanoCliente>();
        }
    }
}
