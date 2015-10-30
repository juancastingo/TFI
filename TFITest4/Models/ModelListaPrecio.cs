using BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelListaPrecio
    {
        [Key]
        public int IDListaPrecio { get; set; }
        [Required]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        public string Detalle { get; set; }
        [Required]
        [Display(Name = "FechaDesde", ResourceType = typeof(Language))]
        public System.DateTime FechaDesde { get; set; }
        [Display(Name = "activo", ResourceType = typeof(Language))]
        public bool Activo { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }
        public ICollection<BIZPrecioDetalle> PrecioDetalle { get; set; }
    }
}