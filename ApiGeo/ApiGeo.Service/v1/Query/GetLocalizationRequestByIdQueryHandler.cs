using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GeoApi.Data.Repository.v1;
using GeoApi.Domain.Entities;

namespace GeoApi.Service.v1.Query
{
    public class GetLocalizationRequestByIdQueryHandler : IRequestHandler<GetLocalizationRequestByIdQuery, Localization>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;

        public GetLocalizationRequestByIdQueryHandler(ILocalizationRequestRepository localizationRequestRepository)
        {
            _localizationRequestRepository = localizationRequestRepository;
        }

        public async Task<Localization> Handle(GetLocalizationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _localizationRequestRepository.GetLocalizationRequestByIdAsync(request.Id, cancellationToken);
        }
    }
}
