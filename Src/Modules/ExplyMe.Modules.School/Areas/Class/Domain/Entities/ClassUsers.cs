using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.School.Areas.Class.Domain.Entities
{
    public class ClassUsers : Entity
    {
        public ClassUsers()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public long ClassId { get; set; }
        public long UserId { get; set; }
        public bool IsInstructor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
