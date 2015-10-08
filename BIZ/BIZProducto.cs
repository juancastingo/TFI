using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIZ
{
    public class BIZProducto
    {
        public BIZProducto()
        {
            this.PrecioDetalle = new List<BIZPrecioDetalle>();
            this.ProductoCategoria = new BIZProductoCategoria();
            this.EstadoMisc = new BIZEstado();
        }
    
        public int IDProducto { get; set; }
        [Required]
        public string Nombre { get; set; }
        public Nullable<int> IDProductoCategoria { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<double> CostoActual { get; set; }
        public Nullable<double> CostoContable { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public string UnidadMedida { get; set; }
        public System.DateTime FechaUltimaMod { get; set; }
        public int IDPrecioDetalle { get; set; }
        public double PrecioActual { get; set; }
    
        public virtual BIZEstado EstadoMisc { get; set; }
        public virtual ICollection<BIZPrecioDetalle> PrecioDetalle { get; set; }
        public virtual BIZProductoCategoria ProductoCategoria { get; set; }

    }
}
