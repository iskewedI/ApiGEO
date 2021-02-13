using Geocodificador.Service.v1.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Geocodificador.Service.v1.Services
{
    public class CodificationService : ICodificationService
    {
        private readonly IMediator _mediator;

        public CodificationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<CodificationResponseModel> CodificateLocalization(LocalizationRequestModel localizationRequestModel)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://nominatim.openstreetmap.org") };

            string url = $"/search?street={localizationRequestModel.Calle}&street={localizationRequestModel.Calle}&city={localizationRequestModel.Ciudad}&state={localizationRequestModel.Provincia}&country={localizationRequestModel.Pais}&postalcode={localizationRequestModel.Codigo_Postal}&format=json";

            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "C# App");

            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            CodificationResponseModel responseModel = JsonConvert.DeserializeObject<List<CodificationResponseModel>>(content).FirstOrDefault();

            return responseModel;
        }
    }
}
