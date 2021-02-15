using System;

namespace Geocodificador.Service.v1.Models
{
    public class LocalizationRequestModel
    {
        public Guid Id { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Ciudad { get; set; }

        public string Codigo_Postal { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }
    }
}
