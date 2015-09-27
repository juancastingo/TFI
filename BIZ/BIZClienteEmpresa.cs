using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZClienteEmpresa
    {
        public int IDClienteEmpresa { get; set; }
        public string Nombre { get; set; }
        public int IDEstado { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaUltimaOperacion { get; set; }
        public Nullable<double> Limite { get; set; }
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public Nullable<int> IDTipoIVA { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public BIZDireccion Direccion { get; set; }
        public BIZEstado EstadoMisc { get; set; }
        public BIZTipoIVA TipoIVA { get; set; }
    }
}
