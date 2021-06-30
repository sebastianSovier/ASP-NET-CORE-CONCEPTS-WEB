using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_CONCEPTS_WEB.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contrasena")]
        [Compare("contrasena", ErrorMessage = "contrasena y confirmacion contrasena no son iguales.")]
        public string confirmarContrasena { get; set; }
        [Required]
        public string nombre_completo { get; set; }
        [Required]
        [EmailAddress]
        public string correo { get; set; }

    }
}
