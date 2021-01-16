using System;

namespace ExplyMe.Modules.School.Areas.School.Domain.Entities
{
    public class School
    {
        public School()
        {
            CreatedAt = DateTime.UtcNow;
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
