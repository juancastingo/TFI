using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BIZ;

namespace BLL
{
    public class BLLBitacora
    {
        DALBitacora _Bitacora = new DALBitacora();
        public void guardarBitacora(BIZBitacora bita)
        {
            _Bitacora.saveBitacora(bita);
            //db.Bitacora.Add(bitacora);
            //db.SaveChanges();
        }
        public List<BIZBitacora> obtenerBitacora()
        {
            return _Bitacora.getBitacora();
        }
    }
}
