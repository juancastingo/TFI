using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZProducto
    {
        public BIZProducto()
        {
            this.ProductoCategoria = new BIZProductoCategoria();
        }
        public int IDProducto { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IDProductoCategoria { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<double> CostoActual { get; set; }
        public Nullable<double> CostoContable { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public byte[] TimeStamp { get; set; }
        public string UnidadMedida { get; set; }
        public int IDPrecioDetalle { get; set; }

        public double PrecioActual { get; set; }
        public BIZProductoCategoria ProductoCategoria { get; set; }

    }
}
