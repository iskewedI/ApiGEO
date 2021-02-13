using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoApi.Service.v1.Models
{
    public class CodificationResponseModel
    {
        [JsonProperty("place_id")]
        public string Id { get; set; }

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
