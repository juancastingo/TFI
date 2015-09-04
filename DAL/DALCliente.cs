using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BIZ;
using DAL.ORM;

namespace DAL
{
    public class DALCliente
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public DALCliente() {
            DALAutommaper automapper = new DALAutommaper();
        }

        public List<BIZClienteEmpresa> getAllClienteEmpresa()
        {
            return AutoMapper.Mapper.Map<List<ClienteEmpresa>, List<BIZClienteEmpresa>>(db.ClienteEmpresa.ToList());
            //List<BIZClienteEmpresa> Rlista = new List<BIZClienteEmpresa>();
            ////var TList = db.ClienteEmpresa.Include("").ToList();
            //var TList = db.ClienteEmpresa.ToList();
            //foreach (var tItem in TList)
            //{
            //    var rItem = AutoMapper.Mapper.Map<ClienteEmpresa, BIZClienteEmpresa>(tItem);
            //    //rItem.Provincia = AutoMapper.Mapper.Map<Provincia, BIZClienteEmpresa>(tItem.Provincia);
            //    Rlista.Add(rItem);
            //}
            //return Rlista;
        }
    }
}
