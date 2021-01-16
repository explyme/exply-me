using System;
using System.Collections.Generic;

namespace ExplyMe.Modules.Core.Domain.Entities
{
    public class User
    {
        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Email { get; set; }
        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
