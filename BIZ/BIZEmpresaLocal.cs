using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZEmpresaLocal
    {
        public int IDEmpresaLocal { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        public string CUIT { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }

        public BIZDireccion Direccion { get; set; }
    }
}
