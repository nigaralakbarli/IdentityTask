using IdentityTask.DTOs.User;
using IdentityTask.DTOs.UserRoleDTO;
using IdentityTask.Models;
using IdentityTask.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        var user = await _userManager.FindByIdAsync(userRoleDTO.UserId.ToString());
        if (user == null)
        {
            return false; 
        }

        var roleName = _roleManager.Roles.FirstOrDefault(role => role.Id == userRoleDTO.RoleId)?.Name;
        var userRoles = await _userManager.GetRolesAsync(user);

        if (roleName == null || !roleName.Contains(roleName))
        {
            return false; 
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        return result.Succeeded; 
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordDTO.Email);
        if (user == null)
        {
            return false;
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordDTO.CurrentPassword);

        if (!isPasswordValid)
        {
            return false; 
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

        return result.Succeeded; 
    }

    public async Task<bool> SoftRemoveUser(int userId)
    {
        User user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == userId);

        if (user==null)
        {
            return false;
        }

        user.IsDeleted = true;
     
        
        var result=await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return false;
        }

        return true;    
    }///

    public async Task<bool> HardDeleteUsersync(int userId)
    {
        User user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == userId);

        if (user == null)
        {
            return false;
        }

        var result=await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return false;
        }

        return true;

    }///
}
