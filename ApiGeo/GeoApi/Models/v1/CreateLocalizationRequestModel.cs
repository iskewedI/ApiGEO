using System;
using System.ComponentModel.DataAnnotations;

namespace GeoApi.Models.v1
{
    public class CreateLocalizationRequestModel
    {
        [Required]
        public string Calle { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Ciudad { get; set; }

        [Required]
        public string Codigo_Postal { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Pais { get; set; }
    }
}