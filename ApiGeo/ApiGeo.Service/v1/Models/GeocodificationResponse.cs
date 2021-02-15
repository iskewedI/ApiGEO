using System;

namespace GeoApi.Service.v1.Models
{
    public class GeocodificationResponse
    {
        public Guid Id { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Estado { get; set; }
    }
}
