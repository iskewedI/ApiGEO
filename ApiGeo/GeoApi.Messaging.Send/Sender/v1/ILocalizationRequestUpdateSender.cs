using GeoApi.Domain.Entities;

namespace GeoApi.Messaging.Send.Sender.v1
{
    public interface ILocalizationRequestUpdateSender
    {
        void SendLocalizationRequest(Localization localizationRequest);
    }
}
