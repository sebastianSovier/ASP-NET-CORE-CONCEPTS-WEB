using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_CONCEPTS_WEB.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }

        [Display(Name = "Remember Me")]
        public bool recordar { get; set; }
    }
}
