using HealthChecks.UI.Client;
using Kanbersky.HealthChecks.Models.Concrete;
using Kanbersky.HealthChecks.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Kanbersky.HealthChecks.Extensions
{
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// This method performs redis healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisHealthCheck(this IServiceCollection services, RedisHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddRedis(redisConnectionString: model.RedisConnectionString, 
                              name: model.Name, 
                              failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs mongo healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoHealthCheck(this IServiceCollection services, MongoDBHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddMongoDb(mongodbConnectionString: model.MongoDBConnectionString,
                                name: model.Name,
                                failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs entity framework healthcheck operations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkHealthCheck<T>(this IServiceCollection services, EntityFrameworkHealthChecksModel model) where T : DbContext
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<T>(name: model.Name,
                                          failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs postgresql healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddPostgreSQLHealthCheck(this IServiceCollection services, PostgreSQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddNpgSql(npgsqlConnectionString: model.PostgreSQLConnectionString,
                               healthQuery: model.HealthQuery,
                               name: model.Name,
                               failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs mysql healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySQLHealthCheck(this IServiceCollection services, MySQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddMySql(connectionString: model.MySQLConnectionString,
                              name: model.Name,
                              failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs mssql healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddMsSQLHealthCheck(this IServiceCollection services, MsSQLHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddSqlServer(connectionString: model.MsSQLConnectionString,
                                  healthQuery: model.HealthQuery,
                                  name: model.Name,
                                  failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs elasticsearch healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IServiceCollection AddElasticsearchHealthCheck(this IServiceCollection services, ElasticsearchHealthChecksModel model)
        {
            services.AddHealthChecks()
                    .AddElasticsearch(elasticsearchUri: model.ElasticsearchUrl,
                                      name: model.Name,
                                      failureStatus: model.FailureStatus);

            return services;
        }

        /// <summary>
        /// This method performs uri group healthcheck operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="urlGroups"></param>
        /// <returns></returns>
        public static IServiceCollection AddUrlGroupsHealthCheck(this IServiceCollection services, List<UrlGroupHealthChecksModel> urlGroups)
        {
            var healthchecks = services.AddHealthChecks();
            foreach (var urlGroup in urlGroups)
            {
                healthchecks.AddUrlGroup(uri: new Uri(urlGroup.ApiUrl), 
                                         name: urlGroup.Name, 
                                         failureStatus: urlGroup.FailureStatus);
            }

            return services;
        }

        /// <summary>
        /// This method performs healthcheckui and Webhook notification operations.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthChecksUI(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: This method only work in slack webhook, change and refactor this method when implement another webhook notification integration

            HealthChecksSettings healthChecksSettings = new HealthChecksSettings();
            configuration.GetSection(nameof(HealthChecksSettings)).Bind(healthChecksSettings);
            services.AddSingleton(healthChecksSettings);

            services.AddHealthChecksUI(settings =>
            {
                if (healthChecksSettings.EnableWebHook)
                {
                    settings.AddWebhookNotification(
                    name: healthChecksSettings.Name,
                    uri: healthChecksSettings.Url,
                    payload: healthChecksSettings.Payload,
                    restorePayload: healthChecksSettings.RestorePayload);
                }
            }).AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseKanberskyHealthChecks(this IApplicationBuilder app, string healthUrl = "/healthy")
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI();

                endpoints.MapHealthChecks(healthUrl, new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            return app;
        }
    }
}
