using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ExplyMe.Modules.Core.Domain.Entities
{
    public class Role : IdentityRole<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
