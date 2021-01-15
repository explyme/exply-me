using System.Data.SqlClient;

namespace ExplyMe.Infrastructure.Data
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateReadConnection();
        SqlConnection CreateWriteConnection();
    }
}
