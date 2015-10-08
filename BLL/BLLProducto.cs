using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BIZ;

namespace BLL
{
    public class BLLProducto
    {
        private DALProducto productoWorker = new DALProducto();

        public List<BIZProducto> traerAllProductos()
        {
            return productoWorker.getAllProductos();
        }

        public BIZProducto traerProdXId(int id)
        {
            return productoWorker.getProdByID(id);
        }

        public System.Collections.IEnumerable traerAllProductoCat()
        {
            return productoWorker.getAllProductoCat();
        }

        public void InsertarProducto(BIZProducto producto)
        {
            producto.FechaAlta = DateTime.Now;
            producto.FechaUltimaMod = producto.FechaAlta;
            producto.EstadoMisc = null;
            producto.ProductoCategoria = null;
            producto.PrecioDetalle = null;
            productoWorker.InsertProducto(producto);
        }

        public void ActualizarProducto(BIZProducto producto)
        {
            producto.FechaUltimaMod = DateTime.Now;
            producto.EstadoMisc = null;
            producto.ProductoCategoria = null;
            producto.PrecioDetalle = null;
            productoWorker.UpdateProducto(producto);
        }
    }
}
