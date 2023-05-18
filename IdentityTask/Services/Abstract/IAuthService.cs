using IdentityTask.DTOs.Authentication;
using IdentityTask.Models;

namespace IdentityTask.Services.Abstract;

public interface IAuthService
{
    Task<string> Login(LoginDTO login);
    Task<string> GenerateTokenAsync(User user);
    Task<string> Registration(RegistrationDTO registration);
}
