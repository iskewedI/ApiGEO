using System;

namespace GeoApi.Domain.Entities
{
    public partial class Localization
    {
        public Guid Id { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Ciudad { get; set; }

        public string Codigo_Postal { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Estado { get; set; }
    }
}
