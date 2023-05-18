namespace IdentityTask.Services.Abstract;

public interface IRoleService
{
    Task<bool> AddRoleAsync(string roleName);
}
