using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.School.Areas.School.Domain.Entities;
using ExplyMe.Modules.School.Areas.School.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.School.Services
{
    public class SchoolFinder : ISchoolFinder
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public SchoolFinder(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }
        public async Task<SchoolEntity> FindByIdAsync(long schoolId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [School] WHERE Id = @schoolId";
            var result = await connection.QueryFirstOrDefaultAsync<SchoolEntity>(query, new { schoolId });

            return result;
        }
    }
}
