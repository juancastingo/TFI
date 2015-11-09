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

        public List<BIZLocalidad> ObtenerLocalidades()
        {
            return dirWorker.getAllLocalidades();
        }

        public List<BIZProvincia> getAllProvincias()
        {
            return dirWorker.getAllProvincias();
        }



        public List<BIZPais> getAllPaises()
        {
            return dirWorker.getAllPaises();
        }

        public void insertProvincia(BIZProvincia Provincia)
        {
            dirWorker.insertProvincia(Provincia);
        }

        public BIZProvincia GetProvinciaByID(int id)
        {
            return dirWorker.GetProvinciaByID(id);
        }

        public void UpdateProvincia(BIZProvincia provincia)
        {
            dirWorker.UpdateProvincia(provincia);
        }

        public List<BIZLocalidad> getAllLocalidades()
        {
            return dirWorker.getAllLocalidades();
        }
    }
}
