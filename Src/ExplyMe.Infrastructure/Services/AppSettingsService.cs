using Dapper;
using ExplyMe.Infrastructure.Data;
using ExplyMe.Infrastructure.Domain.Entities;
using ExplyMe.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExplyMe.Infrastructure.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        public ISqlConnectionFactory SqlConnectionFactory { get; }
        public IMemoryCache MemoryCache { get; }
        private const string CacheKey = "APP_SETTINGS";

        public AppSettingsService(
            ISqlConnectionFactory sqlConnectionFactory,
            IMemoryCache memoryCache)
        {
            SqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
            MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public string Get(string key)
        {
            var appSettingsFromCache = MemoryCache.Get<IEnumerable<AppSetting>>(CacheKey);

            if (appSettingsFromCache is null)
                appSettingsFromCache = SeedSettings();

            return appSettingsFromCache.FirstOrDefault(x => x.Key == key)?.Value;
        }

        public IEnumerable<AppSetting> SeedSettings()
        {
            using var connection = SqlConnectionFactory.CreateReadConnection();
            var query = @"SELECT * FROM [AppSettings]";
            var result = connection.Query<AppSetting>(query);

            MemoryCache.Set(CacheKey, result);

            return result;
        }
    }
}
