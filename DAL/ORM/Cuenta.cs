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
    
    public partial class Cuenta
    {
        public Cuenta()
        {
            this.DocumentoTipo = new HashSet<DocumentoTipo>();
            this.DocumentoTipo1 = new HashSet<DocumentoTipo>();
        }
    
        public int IDCuenta { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IDCategoriaContable { get; set; }
        public Nullable<int> IDSubCategoriaContable { get; set; }
    
        public virtual CategoriaContable CategoriaContable { get; set; }
        public virtual SubCategoriaContable SubCategoriaContable { get; set; }
        public virtual ICollection<DocumentoTipo> DocumentoTipo { get; set; }
        public virtual ICollection<DocumentoTipo> DocumentoTipo1 { get; set; }
    }
}
