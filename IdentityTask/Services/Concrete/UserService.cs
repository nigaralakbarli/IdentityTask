using IdentityTask.DTOs.User;
using IdentityTask.DTOs.UserRoleDTO;
using IdentityTask.Models;
using IdentityTask.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace IdentityTask.Services.Concrete;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(
        UserManager<User> userManager, 
        RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AddRoleToUserAsync(UserRoleDTO userRoleDTO)
    {
        // Check if the role exists
        var user = await _userManager.FindByIdAsync(userRoleDTO.UserId.ToString());
        if (user == null)
        {
            return false; // User not found, return false indicating failure
        }

        // Find the role by ID
        var role = await _roleManager.FindByIdAsync(userRoleDTO.RoleId.ToString());
        if (role == null)
        {
            return false; // Role not found, return false indicating failure
        }

        // Add the role to the user
        var result = await _userManager.AddToRoleAsync(user, role.Name);

        return result.Succeeded; // Return true if the role addition was successful, false otherwise
    }

    public async Task<bool> RemoveUserRoleAsync(UserRoleDTO userRoleDTO)
    {
        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userRoleDTO.UserId.ToString());
        if (user == null)
        {
            return false; // User not found, return false indicating failure
        }

        var roleName = _roleManager.Roles.FirstOrDefault(role => role.Id == userRoleDTO.RoleId)?.Name;
        // Find the role by ID
        var roles = await _userManager.GetRolesAsync(user);
        if (roles == null || !roles.Contains(roleName))
        {
            return false; // User does not have the role, return false indicating failure
        }

        // Remove the role from the user
        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        return result.Succeeded; // Return true if the role removal was successful, false otherwise
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
    {
        // Find the user by email
        var user = await _userManager.FindByEmailAsync(changePasswordDTO.Email);
        if (user == null)
        {
            return false; // User not found, return false indicating failure
        }

        // Verify the current password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordDTO.CurrentPassword);
        if (!isPasswordValid)
        {
            return false; // Current password is not valid, return false indicating failure
        }

        // Change the user's password
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

        return result.Succeeded; // Return true if the password change was successful, false otherwise
    }

}
