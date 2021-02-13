using Geocodificador.Domain.Entities;
using MediatR;

namespace Geocodificador.Service.v1.Command
{
    public class CodificateCommand : IRequest<Codification>
    {
        public Codification Codification { get; set; }
    }
}
