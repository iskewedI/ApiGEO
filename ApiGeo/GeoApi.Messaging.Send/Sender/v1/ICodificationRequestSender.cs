using GeoApi.Domain.Entities;

namespace GeoApi.Messaging.Send.Sender.v1
{
    public interface ICodificationRequestSender
    {
        void SendLocalizationRequest(Localization localization);
    }
}
