using Catalogo.Application.DTOs.Autenticacao;

namespace Catalogo.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginRetornoDTO> Login(LoginModel model);
    Task<RetornoDTO> Register(RegisterModel model);
    Task<RetornoDTO> AddUserToRole(string email, string role);
    Task<LoginRetornoDTO> RefreshToken(TokenModel tokenModel);
    Task<RetornoDTO> Revoke(string username);
    Task<RetornoDTO> CreateRole(string role);
}
