using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BIZ;

namespace BLL
{
    public class BLLGeneral
    {
        DALGeneral generalWorker = new DALGeneral();
        public List<BIZEstado> traerEstadoMisc(string p)
        {
            return generalWorker.getEstadoMisc(p);
        }
    }
}
