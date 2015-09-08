using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZPrecioDetalle
    {
        public int IDPrecioDetalle { get; set; }
        public Nullable<int> IDListaPrecio { get; set; }
        public Nullable<int> IDProducto { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<bool> Activo { get; set; }

        public virtual ICollection<BIZDocumentoDetalle> DocumentoDetalle { get; set; }
       // public virtual  ListaPrecio { get; set; }
        public virtual BIZProducto Producto { get; set; }
    }
}
