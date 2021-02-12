using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GeoApi.Data.Repository.v1;
using GeoApi.Domain.Entities;
using System.Collections.Generic;

namespace GeoApi.Service.v1.Query
{
    public class GetLocalizationRequestsQueryHandler : IRequestHandler<GetLocalizationRequestsQuery, List<Localization>>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;

        public GetLocalizationRequestsQueryHandler(ILocalizationRequestRepository localizationRequestRepository)
        {
            _localizationRequestRepository = localizationRequestRepository;
        }

        public async Task<List<Localization>> Handle(GetLocalizationRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _localizationRequestRepository.GetLocalizationRequestsAsync(cancellationToken);
        }
    }
}
