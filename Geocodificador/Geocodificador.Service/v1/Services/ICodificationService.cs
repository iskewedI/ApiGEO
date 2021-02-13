using Geocodificador.Service.v1.Models;

namespace Geocodificador.Service.v1.Services
{
    public interface ICodificationService
    {
        void CodificateLocalization(LocalizationRequestModel localizationRequestModel);
    }
}
