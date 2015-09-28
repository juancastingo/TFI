using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BIZ;
using DAL.ORM;
using AutoMapper;

namespace DAL
{
    public class DALCliente
    {
        private IIDTest2Entities db = new IIDTest2Entities();

        public DALCliente()
        {
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

        public void AddCliente(BIZClienteEmpresa cliente)
        {
            var TCliente = Mapper.Map<BIZClienteEmpresa, ClienteEmpresa>(cliente);
            TCliente.Direccion.ClienteEmpresa = null;
            TCliente.Direccion.EmpresaLocal = null;
            TCliente.Direccion.FechaUltimaMod = DateTime.Now;
            TCliente.Direccion.Localidad = null;
            TCliente.Direccion.Proveedor = null;

            // db.Direccion.Add(TCliente.Direccion);
            //TCliente.Direccion = null;
            TCliente.Documento = null;
            TCliente.EstadoMisc = null;
            TCliente.FechaUltimaMod = TCliente.FechaAlta;
            TCliente.FechaUltimaOperacion = TCliente.FechaAlta;
            TCliente.TipoIVA = null;
            TCliente.Usuario = null;
            db.ClienteEmpresa.Add(TCliente);
            db.SaveChanges();
        }

        public BIZClienteEmpresa getCliente(int id)
        {


            BIZClienteEmpresa oCliente = new BIZClienteEmpresa();
            var TCliente = db.ClienteEmpresa
                .SingleOrDefault(x => x.IDClienteEmpresa == id);
            if (TCliente != null)
            {
                oCliente = Mapper.Map<ClienteEmpresa, BIZClienteEmpresa>(TCliente);
            }
            else
            {
                return null;
            }

            //mappermap

            return oCliente;






        }

        public void updateCliente(BIZClienteEmpresa c)
        {
            var Tcliente = Mapper.Map<BIZClienteEmpresa, ClienteEmpresa>(c);
            Tcliente.IDDireccion = Tcliente.Direccion.IDDireccion;
            var original = db.ClienteEmpresa.Find(c.IDClienteEmpresa);
            var dirOrig = db.Direccion.Find(c.Direccion.IDDireccion);
            db.Entry(dirOrig).CurrentValues.SetValues(c.Direccion);
            Tcliente.FechaAlta = original.FechaAlta;
            Tcliente.FechaUltimaMod = DateTime.Now;
            
            if (original != null)
            {
                
                db.Entry(original).CurrentValues.SetValues(Tcliente);
                db.SaveChanges();
            }

        }
    }
}
