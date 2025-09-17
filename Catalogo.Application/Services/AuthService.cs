using Catalogo.Application.DTOs.Autenticacao;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        var user = await _authRepository.FindByNameAsync(model.UserName!);
        if (user is not null && await _authRepository.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _authRepository.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAcessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;

            _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            await _authRepository.UpdateAsync(user);

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
}
