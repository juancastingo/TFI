using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZBitacora
    {
        public BIZBitacora()
        {
        }
        public BIZBitacora(string _tipo,string Desc, Nullable<int> IDUser,string _ip)
        {
            this.Descripcion = Desc;
            this.IDUsuario = IDUser;
            this.Tipo = _tipo;
            this.IP = _ip;
            this.Fecha = DateTime.Now;
        }
        public int IDBitacora { get; set; }
        public System.DateTime Fecha { get; set; }
        //public string Modulo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public string Tipo { get; set; }
        public string IP { get; set; }

        public virtual BIZUsuario Usuario { get; set; }
    }
}
