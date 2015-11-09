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

        public List<BIZListaPrecio> TraerAllListaPrecio()
        {
            return precioWorker.getAllListaPrecio();
        }



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

        public void CrearDetallePrecio(BIZPrecioDetalle PrecioDetalle)
        {
            PrecioDetalle.FechaAlta = DateTime.Now;
            PrecioDetalle.FechaUltimaMod = DateTime.Now;
            precioWorker.createDetallePrecio(PrecioDetalle);
        }

        public void copiarLista(BIZListaPrecio Lista, double factor)
        {
            precioWorker.CopyList(Lista, factor);
        }

        public List<BIZListaPrecio> getAllListaPrecio()
        {
            return precioWorker.getAllListaPrecio();
        }

        public void CreateListaPrecio(BIZListaPrecio ListaPrecio)
        {
            precioWorker.CreateListaPrecio(ListaPrecio);
        }

        public BIZListaPrecio GetByID(int id)
        {
            return precioWorker.GetByID(id);
        }

        public void UpdateListaPrecio(BIZListaPrecio collection)
        {
            precioWorker.UpdateListaPrecio(collection);
        }

    }
}
