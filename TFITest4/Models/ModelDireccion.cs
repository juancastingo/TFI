using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFITest4.Models
{
    public class ModelDireccion
    {
        public int IDDireccion { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Dpto { get; set; }
        public string Detalle { get; set; }
        public int IDLocalidad { get; set; }
        public byte[] TimeStamp { get; set; }

        public ModelLocalidad Localidad { get; set; }
    }
}