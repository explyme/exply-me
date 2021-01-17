using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.School.Areas.Class.Domain.Entities;
using ExplyMe.Modules.School.Areas.Class.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.School.Services
{
    public class ClassFinder : IClassFinder
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public ClassFinder(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<ClassEntity> FindByIdAsync(long classId)
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [Class] WHERE Id = @classId";
            var result = await connection.QueryFirstOrDefaultAsync<ClassEntity>(query, new { classId });

            return result;
        }

        public async Task<IEnumerable<ClassEntity>> FindAllAsync()
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();

            const string query = @"SELECT * FROM [Class]";
            var result = await connection.QueryAsync<ClassEntity>(query);

            return result;
        }

    }
}
