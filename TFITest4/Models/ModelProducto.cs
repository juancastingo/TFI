using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIZ;
using System.ComponentModel.DataAnnotations;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelProducto
    {
               public ModelProducto()
        {
            this.ProductoCategoria = new BIZProductoCategoria();
            this.EstadoMisc = new BIZEstado();
        }
    
        public int IDProducto { get; set; }
        [Required]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        public string Nombre { get; set; }
        public Nullable<int> IDProductoCategoria { get; set; }
        [Required]
        [Display(Name = "descripcion", ResourceType = typeof(Language))]
        public string Descripcion { get; set; }
        [Display(Name = "imagen", ResourceType = typeof(Language))]
        public string Imagen { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<double> CostoActual { get; set; }
        public Nullable<double> CostoContable { get; set; }
        [Display(Name = "fechaAlta", ResourceType = typeof(Language))]
        public System.DateTime FechaAlta { get; set; }
        public string UnidadMedida { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }
        public int IDPrecioDetalle { get; set; }
        public double PrecioActual { get; set; }

        [Display(Name = "estado", ResourceType = typeof(Language))]
        public virtual BIZEstado EstadoMisc { get; set; }
        public virtual ICollection<BIZPrecioDetalle> PrecioDetalle { get; set; }
        [Display(Name = "Categoria", ResourceType = typeof(Language))]
        public virtual BIZProductoCategoria ProductoCategoria { get; set; }



    }
}