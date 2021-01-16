using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.School.Areas.Class.Domain.Entities
{
    public class ClassEntity : Entity
    {
        public ClassEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public long Id { get; set; }
        public long SchoolId { get; set; }
        public string NameClass { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
