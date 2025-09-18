using Catalogo.Application.DTOs.Autenticacao;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Catalogo.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IAuthRepository _authRepository;

    public AuthService(ITokenService tokenService, IConfiguration configuration, IAuthRepository authRepository)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _authRepository = authRepository;
    }

    public async Task<LoginRetornoDTO?> Login(LoginModel model)
    {
        var usuario = await _authRepository.FindByNameAsync(model.UserName);
        if (usuario is not null && await _authRepository.CheckPasswordAsync(usuario, model.Password))
        {
            var userRoles = await _authRepository.GetRolesAsync(usuario);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("id", usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAcessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();
            usuario.RefreshToken = refreshToken;

            _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            await _authRepository.UpdateAsync(usuario);

            var retorno = new LoginRetornoDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return retorno;
        }
        return null;
    }

    public async Task<RetornoDTO> Register(RegisterModel model)
    {
        var retorno = new RetornoDTO();
        var usuarioExiste = await _authRepository.FindByNameAsync(model.UserName);

        if (usuarioExiste is not null)
            return new RetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Usuário já existe!" };

        var user = new User(model.UserName, model.Email);

        var resultado = await _authRepository.CreateAsync(user, model.Password);

        return new RetornoDTO() { Status = (int)HttpStatusCode.OK, Mensagem = "Usuário criado com sucesso!" };
    }

    public async Task<RetornoDTO> AddUserToRole(string email, string roleName)
    {
        var usuario = await _authRepository.FindByEmailAsync(email);
        if (usuario is null)
            return new RetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Usuário não existe!" };

        var role = await _authRepository.RoleExistsAsync(roleName);
        if (!role)
            return new RetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Role não existe!" };

        await _authRepository.AddToRoleAsync(usuario, roleName);

        return new RetornoDTO() { Status = (int)HttpStatusCode.OK, Mensagem = "Role adicionada ao usuário com sucesso!" };
    }

    public async Task<LoginRetornoDTO> RefreshToken(TokenModel tokenModel)
    {
        string? acessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
        string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken!, _configuration);
        if (principal is null)
            return new LoginRetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Access token/refresh inválido!" };

        var user = await _authRepository.FindByNameAsync(principal.Identity!.Name!);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return new LoginRetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Token ou usuário não encontrado ou refresh token inválido!" };
        }

        var novoToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);
        var novoRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = novoRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenValidityInMinutes"]!));
        await _authRepository.UpdateAsync(user);

        return new LoginRetornoDTO()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(novoToken),
            RefreshToken = novoRefreshToken,
            Expiration = novoToken.ValidTo
        };
    }

    public async Task<RetornoDTO> Revoke(string username)
    {
        var user = await _authRepository.FindByNameAsync(username);
        if (user is null)
            return new RetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Usuário não existe!" };

        user.RefreshToken = null;
        await _authRepository.UpdateAsync(user);

        return new RetornoDTO() { Status = (int)HttpStatusCode.OK, Mensagem = "Sucesso!" };
    }

    public async Task<RetornoDTO> CreateRole(string role)
    {
        var roleExists = await _authRepository.RoleExistsAsync(role);
        if (roleExists)
            return new RetornoDTO() { Status = (int)HttpStatusCode.BadRequest, Mensagem = "Role já existe!" };

        var roleCriada = await _authRepository.CreateRoleAsync(role);
        if (roleCriada == null)
            return new RetornoDTO() { Status = (int)HttpStatusCode.InternalServerError, Mensagem = "Não foi possível cria a Role!" };

        return new RetornoDTO() { Status = (int)HttpStatusCode.OK, Mensagem = "Role criada!" };
    }
}
