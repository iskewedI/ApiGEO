using Geocodificador.Service.v1.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Geocodificador.Service.v1.Command;
using Geocodificador.Domain.Entities;

namespace Geocodificador.Service.v1.Services
{
    public class CodificationService : ICodificationService
    {
        private readonly IMediator _mediator;

        public CodificationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<Codification> CodificateLocalization(LocalizationRequestModel localizationRequestModel)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://nominatim.openstreetmap.org") };

            string url = $"/search?street={localizationRequestModel.Calle}&street={localizationRequestModel.Calle} {localizationRequestModel.Numero}&city={localizationRequestModel.Ciudad}&state={localizationRequestModel.Provincia}&country={localizationRequestModel.Pais}&postalcode={localizationRequestModel.Codigo_Postal}&format=json";

            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "C# App");

            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            Codification codification = JsonConvert.DeserializeObject<List<Codification>>(content).FirstOrDefault();

            codification.Id = localizationRequestModel.Id;

            CodificationResponseCommand codificationResponseCommand = new CodificationResponseCommand
            {
                Codification = codification
            };

            await _mediator.Send(codificationResponseCommand);

            return null;
        }
    }
}
