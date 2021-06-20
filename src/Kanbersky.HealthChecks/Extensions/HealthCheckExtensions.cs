using Kanbersky.HealthChecks.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.HealthChecks.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddRedisHealthCheck(this IServiceCollection services, RedisHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddRedis(redisConnectionString: model.RedisConnectionString, 
                              name: model.Name, 
                              failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddMongoHealthCheck(this IServiceCollection services, MongoDBHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddMongoDb(mongodbConnectionString: model.MongoDBConnectionString,
                                name: model.Name,
                                failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddEntityFrameworkHealthCheck<T>(this IServiceCollection services, EntityFrameworkHealthChecksModel model) where T : DbContext
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<T>(name: model.Name,
                                          failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddPostgreSQLHealthCheck(this IServiceCollection services, PostgreSQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddNpgSql(npgsqlConnectionString: model.PostgreSQLConnectionString,
                               healthQuery: model.HealthQuery,
                               name: model.Name,
                               failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddMySQLHealthCheck(this IServiceCollection services, MySQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddMySql(connectionString: model.MySQLConnectionString,
                              name: model.Name,
                              failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddMsSQLHealthCheck(this IServiceCollection services, MsSQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddSqlServer(connectionString: model.MsSQLConnectionString,
                                  healthQuery: model.HealthQuery,
                                  name: model.Name,
                                  failureStatus: model.FailureStatus);

            return services;
        }

        public static IServiceCollection AddElasticsearchHealthCheck(this IServiceCollection services, ElasticsearchHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddElasticsearch(elasticsearchUri: model.ElasticsearchUrl,
                                      name: model.Name,
                                      failureStatus: model.FailureStatus);

            return services;
        }
    }
}
