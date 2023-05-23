using IdentityTask.Models;
using IdentityTask.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> AddRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return false;
            }

            var role = new Role { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded;
        }

        public List<string> GetRoles()
        {
            return _roleManager.Roles.Select(r=> r.Name).ToList();
        }

        public Task<bool> HardDeleteUsersync(int roleId)
        {
         _roleManager.FindByIdAsync(int )
        }

        public Task<bool> SoftRemoveUser(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}

