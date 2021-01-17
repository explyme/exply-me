using ExplyMe.Modules.School.Areas.Class.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.Class.Services.Interfaces
{
    public interface IClassFinder
    {
        Task<ClassEntity> FindByIdAsync(long classId);
        Task<IEnumerable<ClassEntity>> FindAllAsync();
    }
}
