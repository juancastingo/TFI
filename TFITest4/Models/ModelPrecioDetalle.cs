using BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelPrecioDetalle
    {
        public int IDPrecioDetalle { get; set; }
        [Display(Name = "listaPrecios", ResourceType = typeof(Language))]
        public Nullable<int> IDListaPrecio { get; set; }
        [Display(Name = "producto", ResourceType = typeof(Language))]
        public Nullable<int> IDProducto { get; set; }
        public int IDTipoUsuario { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [Display(Name = "precio", ResourceType = typeof(Language))]
        public Nullable<double> Precio { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        [Display(Name = "activo", ResourceType = typeof(Language))]
        public Nullable<bool> Activo { get; set; }

        // public virtual  ListaPrecio { get; set; }
        public virtual BIZProducto Producto { get; set; }
    }
}