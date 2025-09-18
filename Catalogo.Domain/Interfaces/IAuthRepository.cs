using Catalogo.Domain.Entities;

namespace Catalogo.Domain.Interfaces;

public interface IAuthRepository
{
    Task<User?> FindByNameAsync(string userName);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IList<string>> GetRolesAsync(User user);
    Task UpdateAsync(User user);
    Task<User> CreateAsync(User user, string password);
    Task<User?> FindByEmailAsync(string email);
    Task<bool> RoleExistsAsync(string roleName);
    Task AddToRoleAsync(User user, string roleName);
    Task<Role> CreateRoleAsync(string roleName);
}
