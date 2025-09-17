using Catalogo.Domain.Entities;

namespace Catalogo.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IList<string>> GetRolesAsync(User user);
        Task UpdateAsync(User user);

        Task<User?> FindByEmailAsync(string email);
        Task<User> CreateAsync(User user, string password);

        Task<bool> RoleExistsAsync(string roleName);
        Task AddRoleAsync(User user, string roleName);
        Task CreateRoleAsync(string roleName);
    }
}
