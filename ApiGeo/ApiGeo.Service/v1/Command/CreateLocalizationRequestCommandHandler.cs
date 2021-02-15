using System.Threading;
using System.Threading.Tasks;
using GeoApi.Domain.Entities;
using GeoApi.Data.Repository.v1;
using MediatR;
using GeoApi.Messaging.Send.Sender.v1;

namespace GeoApi.Service.v1.Command
{
    public class CreateLocalizationRequestCommandHandler : IRequestHandler<CreateLocalizationRequestCommand, Localization>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;
        private readonly ICodificationRequestSender _codificationRequestSender;


        public CreateLocalizationRequestCommandHandler(ICodificationRequestSender codificationRequestSender, ILocalizationRequestRepository localizationRequestRepository)
        {
            _localizationRequestRepository = localizationRequestRepository;
            _codificationRequestSender = codificationRequestSender;
        }

        public async Task<Localization> Handle(CreateLocalizationRequestCommand request, CancellationToken cancellationToken)
        {
            Localization localization = await _localizationRequestRepository.AddAsync(request.LocalizationRequest);

            _codificationRequestSender.SendLocalizationRequest(localization);

            return localization;
        }
    }
}