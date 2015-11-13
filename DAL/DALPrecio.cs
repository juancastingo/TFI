using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using AutoMapper;
using System.Data;

namespace DAL
{
    public class DALPrecio
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        public DALPrecio()
        {
            DALAutommaper automapper = new DALAutommaper();
        }
        public List<BIZListaPrecio> getAllListaPrecio()
        {
            try {
            //var list = db.ListaPrecio.ToList();
            var lista = Mapper.Map<List<ListaPrecio>, List<BIZListaPrecio>>(db.ListaPrecio.ToList());
            return lista;
            } catch (Exception ex) { return null; }
        }
        public void CreateListaPrecio(BIZListaPrecio lista)
        {
            var TList = Mapper.Map<BIZListaPrecio, ListaPrecio>(lista);
            db.ListaPrecio.Add(TList);
            db.SaveChanges();
        }
        public BIZListaPrecio GetByID(int IDLista)
        {
            try
            {
                var TLista = db.ListaPrecio
                    .SingleOrDefault(x => x.IDListaPrecio == IDLista);
                var ListaRet = Mapper.Map<ListaPrecio, BIZListaPrecio>(TLista);
                return ListaRet;
            }
            catch (Exception ex) { return null; }
        }
        public void UpdateListaPrecio(BIZListaPrecio Lista)
        {
            try
            {
                ListaPrecio TLista = new ListaPrecio();
                //Tprovincia = Mapper.Map<BIZProvincia, Provincia>(oProvincia);
                TLista.IDListaPrecio = Lista.IDListaPrecio;
                TLista.Activo = Lista.Activo;
                TLista.FechaDesde = Lista.FechaDesde;
                TLista.FechaUltimaMod = DateTime.Now;
                TLista.Detalle = Lista.Detalle;
                db.Entry(TLista).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex) {  }
        }

        public void CopyList(BIZListaPrecio ListaPrecionueva, double factor)
        {
            try
            {
                ListaPrecio ListaT = new ListaPrecio();
                ListaT.Activo = ListaPrecionueva.Activo;
                ListaT.Detalle = ListaPrecionueva.Detalle;
                ListaT.FechaDesde = ListaPrecionueva.FechaDesde;
                ListaT.FechaUltimaMod = DateTime.Now;
                PrecioDetalle pd;
                var ListaACopiar = db.ListaPrecio.SingleOrDefault(x => x.IDListaPrecio == ListaPrecionueva.IDListaPrecio);
                foreach (var d in ListaACopiar.PrecioDetalle)
                {
                    if ((bool)d.Activo)
                    {
                        pd = new PrecioDetalle();
                        pd.FechaAlta = DateTime.Now;
                        pd.ListaPrecio = null;
                        pd.Producto = null;
                        pd.DocumentoDetalle = null;
                        pd.IDPrecioDetalle = d.IDPrecioDetalle;
                        pd.IDProducto = d.IDProducto;
                        pd.Precio = d.Precio * factor;
                        pd.Activo = true;
                        pd.FechaUltimaMod = DateTime.Now;
                        ListaT.PrecioDetalle.Add(pd);
                    }
                }
                db.ListaPrecio.Add(ListaT);
                db.SaveChanges();
            }
            catch (Exception ex) { }
        }


        public List<BIZPrecioDetalle> geAlltListaDetalle() {
            try
            {
                var lista = Mapper.Map<List<PrecioDetalle>, List<BIZPrecioDetalle>>(db.PrecioDetalle.ToList());
                return lista;
            }
            catch (Exception ex) { return null; }
        }

        public BIZPrecioDetalle getPrecioDetalle(int IDPD)
        {
            try
            {
                var TLista = db.PrecioDetalle
                    .SingleOrDefault(x => x.IDPrecioDetalle == IDPD);
                var ListaRet = Mapper.Map<PrecioDetalle, BIZPrecioDetalle>(TLista);
                return ListaRet;
            }
            catch (Exception ex) { return null; }
        }



        public void UpdatePrecioDetalle(int id, bool Activo)
        {
            string query = "UPDATE PrecioDetalle SET Activo='" + Activo + "' WHERE IDPrecioDetalle = " + id;
            db.Database.ExecuteSqlCommand(query);
        }

        public void createDetallePrecio(BIZPrecioDetalle PrecioDetalle)
        {
            //0 = false
            if ((bool)PrecioDetalle.Activo)
            {
                string query = "UPDATE PrecioDetalle SET Activo=0 WHERE IDListaPrecio=" + PrecioDetalle.IDListaPrecio + " AND IDProducto = " + PrecioDetalle.IDProducto;
                db.Database.ExecuteSqlCommand(query);
            }
            var tPrecio = Mapper.Map<BIZPrecioDetalle, PrecioDetalle>(PrecioDetalle);
            db.PrecioDetalle.Add(tPrecio);
            db.SaveChanges();
        }
    }
}
