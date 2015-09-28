using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BIZ;

namespace BLL
{
    public class BLLDireccion
    {
        private DALDireccion dirWorker = new DALDireccion();

        public List<BIZLocalidad> TraerAllLocalidades()
        {
            return dirWorker.getAllLocalidades();
        }

    }
}
