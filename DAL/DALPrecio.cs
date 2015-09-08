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
            var list = db.ListaPrecio.ToList();
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
    }
}
