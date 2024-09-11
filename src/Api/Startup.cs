using CallCenterAgentManager.Api.Controllers;
using CallCenterAgentManager.API.Swagger;
using CallCenterAgentManager.Application;
using CallCenterAgentManager.Application.AutoMapper.Mappings;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Data.Context.Document;
using CallCenterAgentManager.Data.Context.Relational;
using CallCenterAgentManager.Data.Repositories;
using CallCenterAgentManager.Data.Repositories.Factory;
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Service;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharpCompress.Common;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;



namespace CallCenterAgentManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName.ToLower() == "development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(configuration =>
            {
                configuration.SwaggerEndpoint($"/swagger/{Settings.SwaggerAPIVersion}/swagger.json", Settings.SwaggerAPIName);
                configuration.DocExpansion(DocExpansion.None);
                configuration.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSwagger();
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSettings();
            ConfigureSwagger(services);

            services.AddControllers();

            #region Database Configuration
            if (Settings.UseNoSqlDatabase)
            {
                // MongoDB
                services.AddSingleton<DocumentDbContext>(sp => new DocumentDbContext(
                    Settings.NoSqlDbConnectionString,
                    Settings.NoSqlDatabaseName));
            }
            else
            {
                // PostgreSQL
                services.AddDbContext<RelationalDbContext>(options =>
                    options.UseNpgsql(Settings.RelationalDbConnectionString));
            }
            #endregion

            #region AutoMapper and Domain
            services.AddAutoMapper(typeof(RequestsProfile), typeof(ResponseProfile));
            
            services.AddTransient(typeof(IServiceBase<Domain.Entities.AgentBase<Guid>, Guid>), typeof(ServiceBase<Domain.Entities.AgentBase<Guid>, Guid>));
            services.AddTransient(typeof(IServiceBase<Domain.Entities.EventBase<Guid>, Guid>), typeof(ServiceBase<Domain.Entities.EventBase<Guid>, Guid>));
            services.AddTransient(typeof(IServiceBase<Domain.Entities.QueueBase<Guid>, Guid>), typeof(ServiceBase<Domain.Entities.QueueBase<Guid>, Guid>));
            #endregion

            #region Application Layer
            services.AddTransient(typeof(IApplicationBase<,>), typeof(ApplicationBase<,>));
            services.AddTransient<IAgentApplication, AgentApplication>();
            services.AddTransient<IAdminApplication, AdminApplication>();
            services.AddTransient<IEventApplication, EventApplication>();
            services.AddTransient<IQueueApplication, QueueApplication>();
            #endregion

            #region Services Layer
            services.AddSingleton<StrategyFactory>();
            services.AddTransient(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
            services.AddTransient(typeof(IDataStrategy<,>), typeof(DocumentStrategy<,>));
            services.AddTransient(typeof(IDataStrategy<,>), typeof(RelationalStrategy<,>));

            services.AddTransient(typeof(IServiceBase<Domain.Entities.Relational.Agent, Guid>), typeof(AgentService<Domain.Entities.Relational.Agent, Guid>));
            services.AddTransient(typeof(IServiceBase<Domain.Entities.Document.Agent, string>), typeof(AgentService<Domain.Entities.Document.Agent, string>));
            
            services.AddTransient(typeof(IServiceBase<Domain.Entities.AgentBase<Guid>, Guid>), typeof(AgentService<Domain.Entities.AgentBase<Guid>, Guid>));
            services.AddTransient(typeof(IServiceBase<Domain.Entities.AgentBase<string>, string>), typeof(AgentService<Domain.Entities.AgentBase<string>, string>));

            services.AddTransient(typeof(IEventService<Domain.Entities.Relational.Event, Guid>), typeof(EventService<Domain.Entities.Relational.Event, Guid>));
            services.AddTransient(typeof(IEventService<Domain.Entities.Document.Event, string>), typeof(EventService<Domain.Entities.Document.Event, string>));

            services.AddTransient(typeof(IQueueService<Domain.Entities.Relational.Queue, Guid>), typeof(QueueService<Domain.Entities.Relational.Queue, Guid>));
            services.AddTransient(typeof(IQueueService<Domain.Entities.Document.Queue, string>), typeof(QueueService<Domain.Entities.Document.Queue, string>));

            #endregion

            #region Repository Layer
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(DocumentRepositoryBase<,>));
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RelationalRepositoryBase<,>));


            services.AddTransient<Domain.Repository.Relational.IQueueRepository, Data.Repositories.Relational.QueueRepository>();
            services.AddTransient<Domain.Repository.Document.IQueueRepository, Data.Repositories.Document.QueueRepository>();
            #endregion

            services.AddControllers();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<FormDataOperationFilter>();
                options.SwaggerDoc(Settings.SwaggerAPIVersion, new OpenApiInfo
                {
                    Title = Settings.SwaggerAPIName,
                    Description = $"{Settings.SwaggerAPIName} - {Settings.SwaggerAPIVersion}",
                    Version = Settings.SwaggerAPIVersion
                });
            });
            services.AddSwaggerGenNewtonsoftSupport(); 
        }

        private void ConfigureSettings()
        {
            #region DataBase Settings
            Settings.RelationalDbConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            Settings.NoSqlDbConnectionString = Configuration.GetConnectionString("MongoDbConnection");
            Settings.NoSqlDatabaseName = Configuration["ConnectionStrings:MongoDbName"];
            Settings.UseNoSqlDatabase = bool.Parse(Configuration["DatabaseSettings:UseNoSqlDatabase"]);
            #endregion

            #region Swagger Settings
            Settings.SwaggerAPIName = $"{Configuration["Swagger:APIName"]}";
            Settings.SwaggerAPIVersion = $"{Configuration["Swagger:APIVersion"]}";
            #endregion
        }
    }
}
