using System.Collections.Generic;
using GeoApi.Domain.Entities;
using MediatR;

namespace GeoApi.Service.v1.Query
{
    public class GetLocalizationRequestsQuery : IRequest<List<Localization>>
    { 
    }
}
