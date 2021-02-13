using Geocodificador.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geocodificador.Service.v1.Command
{
    public class CodificateCommandHandler : IRequestHandler<CodificateCommand, Codification>
    {
        public Task<Codification> Handle(CodificateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
