using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIZ;

namespace BLL
{
    public class BLLCliente
    {
        DAL.DALCliente clienteWorker = new DAL.DALCliente();

        public List<BIZClienteEmpresa> ObtenerClienteEmpresas()
        {
            return clienteWorker.getAllClienteEmpresa();
        }

        public void agregarCliente(BIZClienteEmpresa cliente)
        {
            clienteWorker.AddCliente(cliente);
        }

        public BIZClienteEmpresa ObtenerCliente(int id)
        {
            return clienteWorker.getCliente(id);
        }

        public void updateCliente(BIZClienteEmpresa c)
        {
            clienteWorker.updateCliente(c);
        }

    }
}
