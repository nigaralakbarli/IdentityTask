﻿using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Models;

public class Role : IdentityRole<int>
{
    public bool IsDeleted { get; set; } = false;
   
}
