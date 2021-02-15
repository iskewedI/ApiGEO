using GeoApi.Domain.Entities;
using MediatR;

namespace GeoApi.Service.v1.Command
{
    public class UpdateLocalizationRequestCommand : IRequest
    {
        public Localization Localization { get; set; }
    }
}
