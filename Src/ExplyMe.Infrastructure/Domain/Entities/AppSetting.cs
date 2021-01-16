using ExplyMe.Infrastructure.Data;

namespace ExplyMe.Infrastructure.Domain.Entities
{
    public class AppSetting : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
