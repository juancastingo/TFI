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
    }
}
