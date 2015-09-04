using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIZ;

namespace TFITest4.Models
{
    public class ModelClienteEmpresa
    {
        public int IDClienteEmpresa { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaUltimaOperacion { get; set; }
        public Nullable<double> Limite { get; set; }
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        //public Nullable<int> TipoIVA { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public ModelDireccion Direccion { get; set; }
        public BIZEstado EstadoMisc { get; set; }
    }
}