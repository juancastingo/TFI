using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZDireccion
    {
        public int IDDireccion { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Dpto { get; set; }
        public string Detalle { get; set; }
        public int IDLocalidad { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<BIZClienteEmpresa> ClienteEmpresa { get; set; }
        public virtual BIZLocalidad Localidad { get; set; }
        //public virtual ICollection<EmpresaLocal> EmpresaLocal { get; set; }
        //public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
