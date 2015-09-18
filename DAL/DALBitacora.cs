using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL.ORM;
using AutoMapper;

namespace DAL
{
    public class DALBitacora
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        //public void saveBitacora(Bitacora bitacora)
        //{
        //    db.Bitacora.Add(bitacora);
        //    db.SaveChanges();
        //}
        public DALBitacora()
        {
            DALAutommaper automapper = new DALAutommaper();
        }

        public void saveBitacora(BIZBitacora b)
        {
            var Tb = AutoMapper.Mapper.Map<BIZBitacora, Bitacora>(b);
            db.Bitacora.Add(Tb);
            db.SaveChanges();
        }

        public List<BIZBitacora> getBitacora()
        {
            var tBit = db.Bitacora.Include("Usuario").ToList();
            //var rList = AutoMapper.Mapper.Map<List<Bitacora>, List<BIZBitacora>>(tBit);
            List<BIZBitacora> rList = new List<BIZBitacora>();
            BIZBitacora rBit;
            foreach (var b in tBit)
            {
                rBit = new BIZBitacora();
                rBit.Descripcion = b.Descripcion;
                rBit.Fecha = (DateTime)b.Fecha;
                rBit.IDBitacora = b.IDBitacora;
                rBit.IDUsuario = b.IDUsuario;
                rBit.Tipo = b.Tipo;
                if (b.IDUsuario != null)
                {
                    rBit.Usuario = Mapper.Map<Usuario, BIZUsuario>(b.Usuario);
                }
                rList.Add(rBit);
            }
            return rList;
            //return new List<BIZBitacora>();
        }
    }
}
