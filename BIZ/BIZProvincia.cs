using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BIZ
{
   public class BIZProvincia
    {

        public BIZProvincia()
        {
            //this.Localidad = new HashSet<BIZLocalidad>();
            this.Pais = new BIZPais();
        }
        [Key]
        public int IDProvincia { get; set; }
       
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
        public int IDPais { get; set; }
        //public Nullable<int> IDPais { get; set; }
        public ICollection<BIZLocalidad> Localidad { get; set; }
        
        public virtual BIZPais Pais { get; set; }



    }
}
