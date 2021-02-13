using System;
using System.ComponentModel.DataAnnotations;


namespace Geocodificador.Models.v1
{
    public class CodificationModel
    {
        [Required] public string X { get; set; }
    }
}
