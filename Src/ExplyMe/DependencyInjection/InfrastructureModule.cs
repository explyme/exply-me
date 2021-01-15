using ExplyMe.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExplyMe.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static void AddInfrastructureModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ISqlConnectionFactory>(provider =>
            {
                return new SqlConnectionFactory(
                    readConnectionString: configuration.GetConnectionString("DBReadConnectionString"),
                    writeConnectionString: configuration.GetConnectionString("DBWriteConnectionString"));
            });
        }
    }
}
