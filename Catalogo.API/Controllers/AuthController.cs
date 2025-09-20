using Catalogo.Application.DTOs.Autenticacao;
using Catalogo.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var retorno = await _authService.Login(model);

        if (retorno is not null)
            return Ok(retorno);

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            var retorno = await _authService.Register(model);

            if(retorno.Status != (int)System.Net.HttpStatusCode.OK)
                return StatusCode(retorno.Status, retorno.Mensagem);

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    [Route("refresh-token")]
    [ApiExplorerSettings(IgnoreApi = true)] // Não exibe a rota no swagger
    public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
    {
        if (tokenModel is null)
            return BadRequest("É necessário informar o AccessToken e Refresh Token");

        try
        {
            var retorno = await _authService.RefreshToken(tokenModel);

            if (retorno.Status != (int)System.Net.HttpStatusCode.OK)
                return StatusCode(retorno.Status, retorno.Mensagem);

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        try
        {
            var retorno = await _authService.Revoke(username);

            if (retorno.Status != (int)System.Net.HttpStatusCode.OK)
                return StatusCode(retorno.Status, retorno.Mensagem);

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    [Route("create-role")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> CreateRole(string role)
    {
        try
        {
            var retorno = await _authService.CreateRole(role);

            if (retorno.Status != (int)System.Net.HttpStatusCode.OK)
                return StatusCode(retorno.Status, retorno.Mensagem);

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost]
    [Route("add-user-role")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> AddUserToRole(string email, string role)
    {
        try
        {
            var retorno = await _authService.AddUserToRole(email, role);

            if (retorno.Status != (int)System.Net.HttpStatusCode.OK)
                return StatusCode(retorno.Status, retorno.Mensagem);

            return Ok(retorno);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
        }
    }
}
