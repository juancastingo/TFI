using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIZ;
using System.ComponentModel.DataAnnotations;
using TFITest4.Resources;

namespace TFITest4.Models
{
    public class ModelClienteEmpresa
    {
        
        public int IDClienteEmpresa { get; set; }
        [Required]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        public string Nombre { get; set; }
        [Display(Name = "estado", ResourceType = typeof(Language))]
        public int IDEstado { get; set; }
        [Required]
        [Display(Name = "limite", ResourceType = typeof(Language))]
        public Nullable<double> Limite { get; set; }
        [Required]
        public string CUIT { get; set; }
        [Display(Name = "fechaAlta", ResourceType = typeof(Language))]
        public System.DateTime FechaAlta { get; set; }
        [Required]
        [Display(Name = "razonSocial", ResourceType = typeof(Language))]
        public string RazonSocial { get; set; }
        [Required]
        [Display(Name = "nombreFantasia", ResourceType = typeof(Language))]
        public string NombreFantasia { get; set; }
        [Display(Name = "tipoIVA", ResourceType = typeof(Language))]
        public Nullable<int> IDTipoIVA { get; set; }
        public Nullable<int> IDDireccion { get; set; }
        [Required]
        [Display(Name = "Telefono", ResourceType = typeof(Language))]
        public string Telefono { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int IDLocalidad { get; set; }
        [Display(Name = "direccion", ResourceType = typeof(Language))]
        public BIZDireccion Direccion { get; set; }
        [Required]
        [Display(Name = "calle", ResourceType = typeof(Language))]
        public string Calle { get; set; }
        [Required]
        [Display(Name = "numero", ResourceType = typeof(Language))]
        public int Numero { get; set; }
        [Display(Name = "piso", ResourceType = typeof(Language))]
        public int Piso { get; set; }
        [Display(Name = "dpto", ResourceType = typeof(Language))]
        public string Dpto { get; set; }
        [Display(Name = "detalle", ResourceType = typeof(Language))]
        public string Detalle { get; set; }
        [Display(Name = "estado", ResourceType = typeof(Language))]
        public BIZEstado EstadoMisc { get; set; }
        [Display(Name = "tipoIVA", ResourceType = typeof(Language))]
        public BIZTipoIVA TipoIVA { get; set; }

    }
}