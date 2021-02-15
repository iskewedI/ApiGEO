using AutoMapper;
using GeoApi.Domain.Entities;
using GeoApi.Models.v1;
using GeoApi.Service.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeoApi.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        private Localization TrimValues(Localization localization)
        {
            PropertyInfo[] props = localization.GetType().GetProperties();

            foreach(PropertyInfo prop in props)
            {
                if(prop.PropertyType == typeof(string))
                {
                    string propertyValue = prop.GetValue(localization) as string;

                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        prop.SetValue(localization, propertyValue.Trim());
                    }
                }
            }

            return localization;
        }

        public MappingProfile()
        {
            CreateMap<CreateLocalizationRequestModel, Localization>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Localization, Localization>().BeforeMap((source, destination) => TrimValues(source));
            
            CreateMap<Localization, LocalizationResponse>().BeforeMap((source, destination) => TrimValues(source));

            CreateMap<Localization, GeocodificationResponse>().BeforeMap((source, destination) => TrimValues(source));

        }
    }
}
