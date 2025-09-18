using System.ComponentModel.DataAnnotations;

namespace Catalogo.Application.DTOs.Autenticacao;

public class RegisterModel
{
    [Required(ErrorMessage = "O campo UserName é obrigatório.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Password é obrigatório.")]
    public string Password { get; set; }
}
