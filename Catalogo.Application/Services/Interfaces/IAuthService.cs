using Catalogo.Application.DTOs.Autenticacao;

namespace Catalogo.Application.Services.Interfaces;

public interface IAuthService
{
    Task<LoginRetornoDTO> Login(LoginModel loginModel);
}
