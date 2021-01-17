using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.School.Areas.School.Domain.Entities
{
    public class SchoolEntity: Entity
    {
        public SchoolEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsBlocked = false;
        }
        public long Id { get; set; }
        public string FriendlyUrl{ get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string LogoUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
