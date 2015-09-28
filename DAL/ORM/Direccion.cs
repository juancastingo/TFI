//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Direccion
    {
        public Direccion()
        {
            this.ClienteEmpresa = new HashSet<ClienteEmpresa>();
            this.EmpresaLocal = new HashSet<EmpresaLocal>();
            this.Proveedor = new HashSet<Proveedor>();
        }
    
        public int IDDireccion { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Dpto { get; set; }
        public string Detalle { get; set; }
        public Nullable<int> IDLocalidad { get; set; }
        public Nullable<System.DateTime> FechaUltimaMod { get; set; }
    
        public virtual ICollection<ClienteEmpresa> ClienteEmpresa { get; set; }
        public virtual Localidad Localidad { get; set; }
        public virtual ICollection<EmpresaLocal> EmpresaLocal { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
