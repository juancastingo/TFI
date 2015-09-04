using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZListaPrecio
    {

        public int IDListaPrecio { get; set; }
        public string Detalle { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<bool> Activo { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }

        public ICollection<BIZPrecioDetalle> PrecioDetalle { get; set; }




    }
}
