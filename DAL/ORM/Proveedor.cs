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
    
    public partial class Proveedor
    {
        public Proveedor()
        {
            this.Documento = new HashSet<Documento>();
        }
    
        public int IDProveedor { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IDDireccion { get; set; }
    
        public virtual Direccion Direccion { get; set; }
        public virtual ICollection<Documento> Documento { get; set; }
    }
}
