using AutoMapper;
using IdentityTask.DTOs.Authentication;
using IdentityTask.Models;
using IdentityTask.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityTask.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<User> userManager, 
        RoleManager<Role> roleManager,
        IConfiguration config,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _mapper = mapper;
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var role = await _userManager.GetRolesAsync(user);


        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role.FirstOrDefault())
        };

        var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddMinutes(15),
        signingCredentials: credentials); ;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> Login(LoginDTO login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
        {
            return await GenerateTokenAsync(user);
        }
        else
        {
            return "Invalid email or password.";
        }
    }

    public async Task<string> Registration(RegistrationDTO registrationDTO)
    {
        var user = _mapper.Map<User>(registrationDTO);

        var result = await _userManager.CreateAsync(user, registrationDTO.Password);

        if (result.Succeeded)
        {
            return "User registration successful.";
        }
        else
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return "User registration failed. Errors: " + string.Join(", ", errors);
        }
    }
}
