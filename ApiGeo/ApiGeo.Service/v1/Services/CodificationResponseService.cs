using GeoApi.Domain.Entities;
using GeoApi.Service.v1.Command;
using GeoApi.Service.v1.Models;
using GeoApi.Service.v1.Query;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GeoApi.Service.v1.Services
{
    public class CodificationResponseService : ICodificationResponseService
    {
        //private readonly IMediator _mediator;
        private IServiceScopeFactory _serviceScopeFactory;

        public CodificationResponseService(IServiceScopeFactory serviceScopeFactory)
        {
            //_mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async void UpdateLocalizationInfo(CodificationResponseModel codificationResponseModel)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            Localization localizationRequest = await mediator.Send( new GetLocalizationRequestByIdQuery
            { 
                Id = codificationResponseModel.Id
            });

            if(localizationRequest == null)
            {
                return;
            }

            localizationRequest.Latitud = codificationResponseModel.Latitude;
            localizationRequest.Longitud = codificationResponseModel.Longitude;

            localizationRequest.Estado = Geocodification_Status.Terminado;

            await mediator.Send(new UpdateLocalizationRequestCommand { 
                Localization = localizationRequest
            });
        }
    }
}
