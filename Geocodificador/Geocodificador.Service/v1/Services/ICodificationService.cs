using Geocodificador.Service.v1.Models;
using System.Threading.Tasks;

namespace Geocodificador.Service.v1.Services
{
    public interface ICodificationService
    {
        Task<CodificationResponseModel> CodificateLocalization(LocalizationRequestModel localizationRequestModel);
    }
}
