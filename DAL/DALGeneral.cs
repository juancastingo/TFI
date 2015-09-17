using DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALGeneral
    {
        private IIDTest2Entities db = new IIDTest2Entities();
        public DALGeneral()
        {
            DALAutommaper automapper = new DALAutommaper();
        }

        public BIZ.BIZEmpresaLocal getClienteEmpresa() 
        {
            var empresa = db.EmpresaLocal
                .Where(b => b.IDEmpresaLocal == 1).FirstOrDefault();

            var emp = AutoMapper.Mapper.Map<EmpresaLocal, BIZ.BIZEmpresaLocal>(empresa);
            return emp;
        }
    }
}
