using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZDocumento
    {
        public BIZDocumento()
        {
            this.DocumentoDetalle = new List<BIZDocumentoDetalle>();
        }
        public int IDDocumento { get; set; }
        public Nullable<int> NrDocumento { get; set; }
        public Nullable<int> IDDocumentoTipo { get; set; }
        public Nullable<System.DateTime> FechaEmision { get; set; }
        public Nullable<System.DateTime> FechaContable { get; set; }
        public Nullable<int> IDClienteEmpresa { get; set; }
        public Nullable<int> IDProveedor { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<double> Monto { get; set; }
        public double CCStatus { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<int> IDUsuarioCreacion { get; set; }
        public Nullable<int> IDDocumentoRef { get; set; }
        public Nullable<int> IDEmpresaLocal { get; set; }
        public Nullable<int> CodigoExterno { get; set; }
        public string IPCreacion { get; set; }
        public Nullable<int> IDUsuarioUltimaModificacion { get; set; }
        public Nullable<System.DateTime> FechaUltimaModificacion { get; set; }
        public string IPUltimaModificacion { get; set; }
        public string Detalle { get; set; }

        public  BIZDocumentoTipo DocumentoTipo { get; set; }
        public virtual BIZEstado EstadoMisc { get; set; }
        //public virtual Proveedor Proveedor { get; set; }
        public BIZEmpresaLocal EmpresaLocal { get; set; }
        public BIZUsuario Usuario { get; set; }
        public BIZUsuario Usuario1 { get; set; }
        public ICollection<BIZDocumentoDetalle> DocumentoDetalle { get; set; }
        public BIZClienteEmpresa ClienteEmpresa { get; set; }
    }
}
