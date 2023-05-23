using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Models;

public class User : IdentityUser<int>
{

    public bool IsDeleted { get; set; } = false;
   
}
