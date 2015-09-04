using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace TFITest4.Models
{
    public class ModelLocalidad
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IDLocalidad { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string CodigoPostal { get; set; }

        public int IDProvincia { get; set; }

        public BIZ.BIZProvincia Provincia {get; set;}



    }
}