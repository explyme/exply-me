﻿using ExplyMe.Modules.School.Areas.School.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.School.Services.Interfaces
{
    public interface ISchoolFinder
    {
        Task<SchoolEntity> FindByIdAsync(long schoolId);
        Task<IEnumerable<SchoolEntity>> FindAllAsync();
    }
}
