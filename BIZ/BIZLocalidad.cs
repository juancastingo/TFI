using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZLocalidad
    {

        public BIZLocalidad()
        {
            this.Provincia = new BIZProvincia();
        }

        public int IDLocalidad { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public int IDProvincia { get; set; }

        public BIZProvincia Provincia { get; set; }


    }
}
