﻿using Microsoft.AspNetCore.Identity;

namespace ExplyMe.Modules.Core.Domain.Entities
{
    public class UserRole : IdentityUserRole<long>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
