using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BIZ;
using System.ComponentModel.DataAnnotations;
using TFITest4.Resources;


namespace TFITest4.Models
{
    public class ModelPais
    {
        [Key]
        public int IDPais { get; set; }

        [Required]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        [StringLength(10, MinimumLength = 3)]
        public string Nombre { get; set; }


    }
}