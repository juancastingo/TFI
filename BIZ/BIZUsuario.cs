using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class BIZUsuario
    {

        public BIZUsuario()
        {
            this.TipoUsuario = new BIZTipoUsuario();
            this.EstadoMisc = new BIZEstado();
        }

        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
        public int IDEstado { get; set; }
        public int IDClienteEmpresa { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Idioma { get; set; }
        public int IDTipoUsuario { get; set; }
        public DateTime FechaUltimaMod { get; set; }


        public BIZClienteEmpresa ClienteEmpresa { get; set; }
        public ICollection<BIZDocumento> Documento { get; set; }
        public BIZTipoUsuario TipoUsuario { get; set; }
        public BIZEstado EstadoMisc { get; set; }








    }
}
