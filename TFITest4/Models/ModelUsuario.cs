using BIZ;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelUsuario
    {
        public ModelUsuario()
        {
            this.TipoUsuario = new BIZTipoUsuario();
            this.EstadoMisc = new BIZEstado();
        }

        [Key]
        public int IDUsuario { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        public string Nombre { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [Display(Name = "NombreUsuario", ResourceType = typeof(Language))]
        [StringLength(15, ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "MaxLength")]
        public string Usuario1 { get; set; }

        public string Password { get; set; }
        public int IDEstado { get; set; }
        public int IDClienteEmpresa { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo Electrónico invalido")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
       ErrorMessageResourceName = "Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Formato Teléfono Invalido")]
        [Display(Name = "Telefono", ResourceType = typeof(Language))]
        public string Telefono { get; set; }

        public DateTime FechaAlta { get; set; }
        public string Idioma { get; set; }

        [Display(Name = "tipoUsuario", ResourceType = typeof(Language))]
        public int IDTipoUsuario { get; set; }
        public DateTime FechaUltimaMod { get; set; }

        public BIZClienteEmpresa ClienteEmpresa { get; set; }
        public ICollection<BIZDocumento> Documento { get; set; }
        public BIZTipoUsuario TipoUsuario { get; set; }
        public BIZEstado EstadoMisc { get; set; }
    }
}