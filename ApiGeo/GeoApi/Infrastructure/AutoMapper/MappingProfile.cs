using AutoMapper;
using GeoApi.Domain.Entities;
using GeoApi.Models.v1;

namespace GeoApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLocalizationRequestModel, Localization>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Localization, LocalizationResponse>();
        }
    }
}
