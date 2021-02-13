using Geocodificador.Domain.Entities;

namespace Geocodificador.Messaging.Send.Sender.v1
{
    public interface ICodificationResponseSender
    {
        void SendCodificationResponse(Codification codification);
    }
}
