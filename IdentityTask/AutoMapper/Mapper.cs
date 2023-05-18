using AutoMapper;
using IdentityTask.DTOs.Authentication;
using IdentityTask.Models;

namespace IdentityTask.AutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<RegistrationDTO, User>();
    }
}
