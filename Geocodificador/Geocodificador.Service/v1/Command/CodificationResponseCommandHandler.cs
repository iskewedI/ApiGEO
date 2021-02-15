using Geocodificador.Domain.Entities;
using Geocodificador.Messaging.Send.Sender.v1;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Geocodificador.Service.v1.Command
{
    public class CodificationResponseCommandHandler : IRequestHandler<CodificationResponseCommand, Codification>
    {
        private readonly ICodificationResponseSender _codificationResponseSender;

        public CodificationResponseCommandHandler(ICodificationResponseSender codificationResponseSender)
        {
            _codificationResponseSender = codificationResponseSender;
        }

        public Task<Codification> Handle(CodificationResponseCommand request, CancellationToken cancellationToken)
        {
            _codificationResponseSender.SendCodificationResponse(request.Codification);

            return null;
        }
    }
}
