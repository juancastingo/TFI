using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZDocumentoDetalle
    {
        public int IDDocumentoDetalle { get; set; }
        public int IDDocumento { get; set; }
        public int IDPrecioDetalle { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }

        public  BIZDocumento Documento { get; set; }
        public  BIZPrecioDetalle PrecioDetalle { get; set; }
    }
}
