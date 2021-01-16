using System;

namespace ExplyMe.Modules.School.Areas.School.Domain.Entities
{
    public class SchoolUser
    {
        public SchoolUser()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public long SchoolId { get; set; }
        public long UserId { get; set; }
        public bool IsInstructor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
