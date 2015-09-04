using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZBitacora
    {
        public int IDBitacora { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Modulo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IDUsuario { get; set; }

        public virtual BIZUsuario Usuario { get; set; }
    }
}
