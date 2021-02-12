using GeoApi.Domain.Entities;
using MediatR;

namespace GeoApi.Service.v1.Command
{
    public class CreateLocalizationRequestCommand : IRequest<Localization>
    {
        public Localization LocalizationRequest { get; set; }
    }
}
