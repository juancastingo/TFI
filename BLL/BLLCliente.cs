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
        DAL.DALCliente D_Cliente = new DAL.DALCliente();
        public List<BIZClienteEmpresa> ObtenerClienteEmpresas()
        {
            return D_Cliente.getAllClienteEmpresa();
        }
    }
}
