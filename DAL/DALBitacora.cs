using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;
using DAL.ORM;

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
            return AutoMapper.Mapper.Map<List<Bitacora>, List<BIZBitacora>>(db.Bitacora.ToList());
        }
    }
}
