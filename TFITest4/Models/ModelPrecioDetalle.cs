using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFITest4.Models
{
    public class ModelPrecioDetalle
    {
        public int IDPrecioDetalle { get; set; }
        public Nullable<int> IDListaPrecio { get; set; }
        public Nullable<int> IDProducto { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<bool> Activo { get; set; }

        // public virtual  ListaPrecio { get; set; }
        public virtual BIZProducto Producto { get; set; }
    }
}