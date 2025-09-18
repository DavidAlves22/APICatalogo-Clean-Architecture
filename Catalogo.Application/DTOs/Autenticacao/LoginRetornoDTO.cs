namespace Catalogo.Application.DTOs.Autenticacao;

public class LoginRetornoDTO : RetornoDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
