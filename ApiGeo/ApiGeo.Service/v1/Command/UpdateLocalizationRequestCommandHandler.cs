using GeoApi.Data.Repository.v1;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoApi.Service.v1.Command
{
    public class UpdateLocalizationRequestCommandHandler : IRequestHandler<UpdateLocalizationRequestCommand>
    {
        private readonly ILocalizationRequestRepository _localizationRequestRepository;

        public UpdateLocalizationRequestCommandHandler(ILocalizationRequestRepository localizationRequestRepository)
        {
            _localizationRequestRepository = localizationRequestRepository;
        }
        public async Task<Unit> Handle(UpdateLocalizationRequestCommand request, CancellationToken cancellationToken)
        {
            await _localizationRequestRepository.UpdateAsync(request.Localization);

            return Unit.Value;
        }
    }
}
