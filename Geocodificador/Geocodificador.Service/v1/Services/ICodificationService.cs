using Geocodificador.Domain.Entities;
using Geocodificador.Service.v1.Models;
using System.Threading.Tasks;

namespace Geocodificador.Service.v1.Services
{
    public interface ICodificationService
    {
        Task<Codification> CodificateLocalization(LocalizationRequestModel localizationRequestModel);
    }
}
