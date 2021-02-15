using GeoApi.Data.Repository.v1;
using GeoApi.Domain.Entities;
using GeoApi.Messaging.Send.Sender.v1;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoApi.Service.v1.Command
{
    public class CodificationRequestCommandHandler : IRequestHandler<CodificationRequestCommand, Localization>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;
        private readonly ICodificationRequestSender _codificationRequestSender;

        public CodificationRequestCommandHandler(ICodificationRequestSender codificationRequestSender, ILocalizationRequestRepository localizationRequestRepository)
        {
            _localizationRequestRepository = localizationRequestRepository;
            _codificationRequestSender = codificationRequestSender;
        }

        public async Task<Localization> Handle(CodificationRequestCommand request, CancellationToken cancellationToken)
        {
            var localizationRequest = await _localizationRequestRepository.GetLocalizationRequestByIdAsync(request.Localization.Id, cancellationToken);
            
            _codificationRequestSender.SendLocalizationRequest(localizationRequest);
            
            return localizationRequest;
        }
    }
}
