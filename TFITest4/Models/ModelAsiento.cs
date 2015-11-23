using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelAsiento
    {
        public int IDDocumento { get; set; }
        public DateTime FechaContable { get; set; }
        public string debe { get; set; }
        public string haber { get; set; }
        public double monto { get; set; }
            
    }
}