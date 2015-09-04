using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZProductoCategoria
    {
        public BIZProductoCategoria()
        {
            this.Producto = new List<BIZProducto>();
        }
        public int IDProductoCategoria { get; set; }
        public string Detalle { get; set; }
        public Nullable<int> Estado { get; set; }

        public  List<BIZProducto> Producto { get; set; }

    }
}
