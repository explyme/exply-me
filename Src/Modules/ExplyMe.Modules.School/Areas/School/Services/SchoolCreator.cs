using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.School.Areas.School.Domain.Entities;
using ExplyMe.Modules.School.Areas.School.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.School.Areas.School.Services
{
    public class SchoolCreator : ISchoolCreator
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public SchoolCreator(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }
        public async Task<SchoolEntity> CreateAsync(SchoolEntity school)
        {
            using var connection = SqlConnectionFactory.CreateWriteConnection();
            const string query =
@"INSERT INTO [School]
    ([FriendlyUrl], [DisplayName], [Bio], [LogoUrl], [IsPublic], [IsBlocked], [CreatedAt])
OUTPUT INSERTED.ID
VALUES
    (@FriendlyUrl, @DisplayName, @Bio, @LogoUrl, @IsPublic, @IsBlocked, @CreatedAt)";

            school.Id = await connection.ExecuteScalarAsync<long>(query, school);

            return school;
        }
    }
}
