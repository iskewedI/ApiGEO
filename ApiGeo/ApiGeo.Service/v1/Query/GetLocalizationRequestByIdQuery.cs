using System;
using GeoApi.Domain.Entities;
using MediatR;

namespace GeoApi.Service.v1.Query
{
    public class GetLocalizationRequestByIdQuery : IRequest<Localization>
    {
        public Guid Id { get; set; }
    }
}
