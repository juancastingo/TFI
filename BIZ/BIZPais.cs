using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BIZ
{
    public class BIZPais
    {
            public int IDPais { get; set; }
            public string Nombre { get; set; }
            public ICollection<BIZProvincia> Provincia { get; set; }
    }
}
