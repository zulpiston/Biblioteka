﻿using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        
    }
}
//xxx