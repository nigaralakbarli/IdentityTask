using IdentityTask.Services.Abstract;
using IdentityTask.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTask.Controllers
{
    [Route("RoleController")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("{roleName}")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            // Add the role to the system
            var result = await _roleService.AddRoleAsync(roleName);
            if (result)
            {
                return Ok("Role added successfully");
            }
            else
            {
                return BadRequest("Failed to add role");
            }
        }
    }
}
