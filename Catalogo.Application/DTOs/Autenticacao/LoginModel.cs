using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Application.DTOs.Autenticacao
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string Password { get; set; }
    }
}
