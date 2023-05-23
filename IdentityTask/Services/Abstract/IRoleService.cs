namespace IdentityTask.Services.Abstract;

public interface IRoleService
{
    Task<bool> AddRoleAsync(string roleName);
    List<string> GetRoles();
    Task<bool> SoftRemoveUser(int roleId);
    Task<bool> HardDeleteUsersync(int roleId);
}
