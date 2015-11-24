using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZCuenta
    {


        public int IDCuenta { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IDCategoriaContable { get; set; }
        public Nullable<int> IDSubCategoriaContable { get; set; }

        public BIZCategoriaContable CategoriaContable { get; set; }
        public BIZSubCategoriaContable SubCategoriaContable { get; set; }


    }
}
