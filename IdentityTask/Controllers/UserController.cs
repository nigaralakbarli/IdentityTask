using IdentityTask.DTOs.User;
using IdentityTask.DTOs.UserRoleDTO;
using IdentityTask.Services.Abstract;
using IdentityTask.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTask.Controllers
{
    [Route("UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("/AddRoleToUser")]
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(UserRoleDTO userRoleDTO)
        {
            // Add the role to the user
            var result = await _userService.AddRoleToUserAsync(userRoleDTO);
            if (result)
            {
                return Ok("Role added to user successfully");
            }
            else
            {
                return BadRequest("Failed to add role to user");
            }
        }


        [Route("/RemoveUserRole")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserRole(UserRoleDTO UserRoleDTO)
        {
            var result = await _userService.RemoveUserRoleAsync(UserRoleDTO);
            if (result)
            {
                return Ok("Role removed from user successfully");
            }
            else
            {
                return BadRequest("Failed to remove role from user");
            }
        }

        [Route("/ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var result = await _userService.ChangePasswordAsync(changePasswordDTO);
            if (result)
            {
                return Ok("Password changed successfully");
            }
            else
            {
                return BadRequest("Failed to change password");
            }
        }
    }
}
