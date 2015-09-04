using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Web.WebPages.Html;
using TFITest4.Resources;

namespace TFITest4.Models
{
    
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool? RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceType=typeof(Resources.Language),
        ErrorMessageResourceName= "Required")]
        [Display(Name = "NombreUsuario", ResourceType = typeof(Language))]
        [StringLength(15, ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "MaxLength")]
        public string Usuario1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [Display(Name = "Nombre", ResourceType = typeof(Language))]
        public string Nombre { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType=typeof(Resources.Language),
        ErrorMessageResourceName= "MinLength")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Language))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "RepeatPassword", ResourceType = typeof(Language))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "RepeatPasswordCheck")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo Electrónico invalido")]
        public string email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Language),
        ErrorMessageResourceName = "Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Formato Teléfono Invalido")]
        [Display(Name = "Telefono", ResourceType = typeof(Language))]
        public int Telefono { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Language),
        //ErrorMessageResourceName = "Required")]
        //public string direccion { get; set; }

        //public SelectList 
        public int IDClienteEmpresa { get; set; }
        public int IDLocalidad { get; set; }
        public int IDTipoUsuario { get; set; }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }


    
}