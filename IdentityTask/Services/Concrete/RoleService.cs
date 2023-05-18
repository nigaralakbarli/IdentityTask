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
            // Check if the role already exists
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return false; // Role already exists, return false indicating failure
            }

            // Create a new role
            var role = new Role { Name = roleName };

            // Add the role to the system
            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded; // Return true if the role creation was successful, false otherwise
        }
    }
}

