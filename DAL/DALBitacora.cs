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
        public void saveBitacora(Bitacora bitacora)
        {
            db.Bitacora.Add(bitacora);
            db.SaveChanges();
        }
    }
}
