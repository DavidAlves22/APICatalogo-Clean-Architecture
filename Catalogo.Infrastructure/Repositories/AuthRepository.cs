using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Catalogo.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User?> FindByNameAsync(string userName)
    {
        var applicationUser = await _userManager.FindByNameAsync(userName);
        return applicationUser is null ? null : MapToDomain(applicationUser);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        return applicationUser != null && await _userManager.CheckPasswordAsync(applicationUser, password);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        return applicationUser is null ? new List<string>() : await _userManager.GetRolesAsync(applicationUser);
    }

    public async Task UpdateAsync(User user)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        if (applicationUser is null) return;

        applicationUser.RefreshToken = user.RefreshToken;
        applicationUser.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;

        await _userManager.UpdateAsync(applicationUser);
    }

    public async Task<User> CreateAsync(User user, string password)
    {
        var applicationUser = new ApplicationUser()
        {
            UserName = user.UserName,
            Email = user.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var retorno = await _userManager.CreateAsync(applicationUser, password);
        if (!retorno.Succeeded)
            throw new Exception($"Erro ao criar usuário: {string.Join(", ", retorno.Errors.Select(e => e.Description))}");

        return MapToDomain(applicationUser);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        return applicationUser is null ? null : MapToDomain(applicationUser);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task AddToRoleAsync(User user, string roleName)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        if (applicationUser is not null)
        {
            var retorno = await _userManager.AddToRoleAsync(applicationUser, roleName);
            if (!retorno.Succeeded)
                throw new Exception($"Erro ao associar Role ao Usuário: {string.Join(", ", retorno.Errors.Select(e => e.Description))}");
        }
    }

    public async Task<Role> CreateRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            throw new Exception($"Role já existe");

        var resultado = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (resultado.Succeeded)
            return new Role(roleName);
        else
            throw new Exception($"Erro ao criar Role: {string.Join(", ", resultado.Errors.Select(e => e.Description))}");
    }

    private static User MapToDomain(ApplicationUser appUser)
    {
        return new User(appUser.UserName!, appUser.Email!)
        {
            RefreshToken = appUser.RefreshToken,
            RefreshTokenExpiryTime = appUser.RefreshTokenExpiryTime
        };
    }
}
