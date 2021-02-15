using AutoMapper;
using Geocodificador.Domain.Entities;
using Geocodificador.Models.v1;

namespace Geocodificador.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CodificationModel, Codification>().ForMember(x => x.Id, opt => opt.MapFrom(src => 1));
        }
    }
}
