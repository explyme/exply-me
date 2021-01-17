using Dapper;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.School.Areas.Class.Domain.Entities;
using ExplyMe.Modules.School.Areas.Class.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.Class.Services
{
    public class ClassCreator : IClassCreator
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }

        public ClassCreator(ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }

        public async Task<ClassEntity> CreateAsync(ClassEntity classEntity)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            const string query =
@"INSERT INTO [Class]
    ([NameClass], [Description], [StartDate], [EndDate], [CreatedAt])
OUTPUT INSERTED.ID
VALUES
    (@NameClass, @Description, @StartDate, @EndDate, @CreatedAt)";
            classEntity.Id = await connection.ExecuteScalarAsync<long>(query, classEntity);

            return classEntity;
        }
    }
}
