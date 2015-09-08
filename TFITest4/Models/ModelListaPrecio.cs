using BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TFITest4.Models
{
    public class ModelListaPrecio
    {
        [Key]
        public int IDListaPrecio { get; set; }
        public string Detalle { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }
        public ICollection<BIZPrecioDetalle> PrecioDetalle { get; set; }
    }
}