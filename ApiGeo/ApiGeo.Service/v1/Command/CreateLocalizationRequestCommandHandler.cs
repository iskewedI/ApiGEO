using System.Threading;
using System.Threading.Tasks;
using GeoApi.Domain.Entities;
using GeoApi.Data.Repository.v1;
using MediatR;

namespace GeoApi.Service.v1.Command
{
    public class CreateLocalizationRequestCommandHandler : IRequestHandler<CreateLocalizationRequestCommand, Localization>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;

        public CreateLocalizationRequestCommandHandler(ILocalizationRequestRepository customerRepository)
        {
            _localizationRequestRepository = customerRepository;
        }

        public async Task<Localization> Handle(CreateLocalizationRequestCommand request, CancellationToken cancellationToken)
        {
            return await _localizationRequestRepository.AddAsync(request.LocalizationRequest);
        }
    }
}