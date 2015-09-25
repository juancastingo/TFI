using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL;

namespace BLL
{
    public class BLLPrecio
    {
        private DALPrecio precioWorker = new DALPrecio();

        public List<BIZPrecioDetalle> traerPrecioDetalles() {
            return precioWorker.geAlltListaDetalle();
        }
        public BIZPrecioDetalle traerPrecioDetalle(int id)
        {
            return precioWorker.getPrecioDetalle(id);
        }

        public void ActualizarPrecioDetalle(int id, bool Activo)
        {
            precioWorker.UpdatePrecioDetalle(id,Activo);
        }
    }
}
