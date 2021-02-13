using MediatR;
using GeoApi.Domain.Entities;

namespace GeoApi.Service.v1.Command
{
    public class CodificationRequestCommand : IRequest<Localization>
    {
        public Localization Localization{ get; set; }
    }
}
