using System.ComponentModel.DataAnnotations;

namespace Catalogo.Application.DTOs.Autenticacao;

public class TokenModel
{
    [Required(ErrorMessage = "Necessário informar o Token")]
    public string AccessToken { get; set; }

    [Required(ErrorMessage = "Necessário informar o Refresh Token")]
    public string RefreshToken { get; set; }
}
