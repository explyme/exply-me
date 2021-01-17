using ExplyMe.Modules.School.Areas.Class.Domain.Entities;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.Class.Services.Interfaces
{
    public interface IClassCreator
    {
        Task<ClassEntity> CreateAsync(ClassEntity classEntity);
    }
}
