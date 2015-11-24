using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZDocumentoTipo
    {

        public int IDDocumentoTipo { get; set; }
        public string NombreDocumento { get; set; }
        public Nullable<int> UltimoNumero { get; set; }
        public string Letra { get; set; }
        public Nullable<int> Sucursal { get; set; }
        public Nullable<int> AfectaStock { get; set; }
        public Nullable<int> AfectaCC { get; set; }
        public Nullable<int> Debito { get; set; }
        public Nullable<int> AfectaContabilidad { get; set; }
        public Nullable<int> Credito { get; set; }
        public Nullable<int> UltimoNumeroManual { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }

        public BIZCuenta Cuenta { get; set; }
        public BIZCuenta Cuenta1 { get; set; }
        public virtual ICollection<BIZDocumento> Documento { get; set; }
    }
}
