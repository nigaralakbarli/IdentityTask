using IdentityTask.DTOs.User;
using IdentityTask.DTOs.UserRoleDTO;
using IdentityTask.Models;

namespace IdentityTask.Services.Abstract;

public interface IUserService
{
    Task<bool> AddRoleToUserAsync(UserRoleDTO userRoleDTO);
    Task<bool> RemoveUserRoleAsync(UserRoleDTO userRoleDTO);
    Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
}
