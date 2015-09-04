using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TFITest4.Models
{
    public class ModelProvincia
    {
        [Key]
        public int IDProvincia { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IDPais { get; set; }
        [Required]
        public String Pais { get; set; }
    }
}