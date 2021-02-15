using Newtonsoft.Json;
using System;

namespace GeoApi.Service.v1.Models
{
    public class CodificationResponseModel
    {
        public Guid Id { get; set; }

        [JsonProperty("place_id")]
        public string Place_Id { get; set; }

        [JsonProperty("osm_id")]
        public string Osm_Id { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("display_name")]
        public string Display_Name { get; set; }
    }
}
