using Catalogo.Application.DTOs.Autenticacao;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var retorno = await _authService.Login(model);       

        if(retorno is not null)
            return Ok(retorno);

        return Unauthorized();
    }

    //[AllowAnonymous]
    //[HttpPost]
    //[Route("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterModel model)
    //{
    //    var userExists = await _userManager.FindByNameAsync(model.UserName!);

    //    if (userExists is not null)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Usuário já existe!" });
    //    }

    //    ApplicationUser user = new ApplicationUser()
    //    {
    //        Email = model.Email,
    //        SecurityStamp = Guid.NewGuid().ToString(),
    //        UserName = model.UserName
    //    };

    //    var result = await _userManager.CreateAsync(user, model.Password!);

    //    if (!result.Succeeded)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = $"Erro ao criar usuário! {string.Join("; ", result.Errors.Select(i => i.Description))}" });
    //    }

    //    return Ok(new { Status = "Success", Message = "Usuário criado com sucesso!" });
    //}

    //[AllowAnonymous]
    //[HttpPost]
    //[Route("refresh-token")]
    //[ApiExplorerSettings(IgnoreApi = true)] // Não exibe a rota no swagger
    //public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
    //{
    //    if (tokenModel is null)
    //    {
    //        return BadRequest("Invalid client request");
    //    }

    //    string? acessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
    //    string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

    //    var principal = _tokenService.GetPrincipalFromExpiredToken(acessToken!, _configuration);
    //    if (principal is null)
    //    {
    //        return BadRequest("Invalid access token/refresh");
    //    }

    //    var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);
    //    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
    //    {
    //        return BadRequest("Invalid token or user not found or refresh token invalid");
    //    }

    //    var novoToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);
    //    var novoRefreshToken = _tokenService.GenerateRefreshToken();

    //    user.RefreshToken = novoRefreshToken;
    //    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenValidityInMinutes"]!));
    //    await _userManager.UpdateAsync(user);

    //    return Ok(new
    //    {
    //        accessToken = new JwtSecurityTokenHandler().WriteToken(novoToken),
    //        refreshToken = novoRefreshToken,
    //        Expiration = novoToken.ValidTo
    //    });
    //}

    //[Authorize]
    //[HttpPost]
    //[Route("revoke/{username}")]
    //public async Task<IActionResult> Revoke(string username)
    //{
    //    var user = await _userManager.FindByNameAsync(username);
    //    if (user is null)
    //        return BadRequest("Invalid user name");

    //    user.RefreshToken = null;
    //    await _userManager.UpdateAsync(user);
    //    return NoContent();
    //}

    //[HttpPost]
    //[Route("create-role")]
    //[Authorize(Policy = "SuperAdminOnly")]
    //public async Task<IActionResult> CreateRole(string roleName)
    //{
    //    var roleExists = await _roleManager.RoleExistsAsync(roleName);
    //    if (roleExists)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Role já existe!" });

    //    var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
    //    if (!result.Succeeded)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = $"Erro ao criar role! {string.Join("; ", result.Errors.Select(i => i.Description))}" });

    //    _logger.LogInformation($"Role {roleName} criada com sucesso.");
    //    return Ok(new { Status = "Success", Message = "Role criada com sucesso!" });
    //}

    //[HttpPost]
    //[Route("add-user-role")]
    //[Authorize(Policy = "SuperAdminOnly")]
    //public async Task<IActionResult> AddUserToRole(string email, string roleName)
    //{
    //    var user = await _userManager.FindByEmailAsync(email);
    //    if (user is null)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Usuário não existe!" });

    //    var roleExists = await _roleManager.RoleExistsAsync(roleName);
    //    if (!roleExists)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Role não existe!" });

    //    var result = await _userManager.AddToRoleAsync(user, roleName);
    //    if (!result.Succeeded)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = $"Erro ao adicionar role ao usuário! {string.Join("; ", result.Errors.Select(i => i.Description))}" });

    //    return Ok(new { Status = "Success", Message = "Role adicionada ao usuário com sucesso!" });
    //}
}
