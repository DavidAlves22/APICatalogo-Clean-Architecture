using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Catalogo.Infrastructure.Repositories
{
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

        //public async Task<User?> FindByEmailAsync(string email)
        //{
        //    var applicationUser = await _userManager.FindByEmailAsync(email);
        //    return applicationUser is null ? null : MapToDomain(applicationUser);
        //}


        //public async Task<User> CreateAsync(User user, string password)
        //{
        //    var identityUser = new ApplicationUser
        //    {
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        RefreshToken = user.RefreshToken,
        //        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
        //    };


        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.UserName
        //    };


        //    var result = await _userManager.CreateAsync(identityUser, password);
        //    if (!result.Succeeded)
        //        throw new Exception($"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        //    return MapToDomain(identityUser);
        //}



        //public async Task<bool> RoleExistsAsync(string roleName)
        //{
        //    return await _roleManager.RoleExistsAsync(roleName);
        //}

        //public async Task AddRoleAsync(User user, string roleName)
        //{
        //    var identityUser = await _userManager.FindByNameAsync(user.UserName);
        //    if (identityUser is not null)
        //    {
        //        await _userManager.AddToRoleAsync(identityUser, roleName);
        //    }
        //}

        //public async Task CreateRoleAsync(string roleName)
        //{
        //    if (!await _roleManager.RoleExistsAsync(roleName))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole(roleName));
        //    }
        //}

        private static User MapToDomain(ApplicationUser appUser)
        {
            return new User(appUser.UserName!, appUser.Email!)
            {
                RefreshToken = appUser.RefreshToken,
                RefreshTokenExpiryTime = appUser.RefreshTokenExpiryTime
            };
        }
    }
}
