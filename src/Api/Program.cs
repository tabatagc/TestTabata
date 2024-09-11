using CallCenterAgentManager.Api.Controllers;
using CallCenterAgentManager.Application;
using CallCenterAgentManager.Application.AutoMapper.Factory;
using CallCenterAgentManager.Application.AutoMapper.Factory.Contracts;
using CallCenterAgentManager.Application.AutoMapper.Mappings;
using CallCenterAgentManager.Application.Contracts;
using CallCenterAgentManager.CrossCutting.Settings;
using CallCenterAgentManager.Data.Context.Document;
using CallCenterAgentManager.Data.Context.Relational;
using CallCenterAgentManager.Data.Repositories;
using CallCenterAgentManager.Data.Repositories.Factory;
using CallCenterAgentManager.Domain.Entities;
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Service;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de Servi�os
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configura��o de Middleware e Rotas
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    ConfigureSettings(configuration);

    services.AddControllers();

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });

    // Swagger Configuration
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(Settings.SwaggerAPIVersion, new OpenApiInfo
        {
            Title = Settings.SwaggerAPIName,
            Description = $"{Settings.SwaggerAPIName} - {Settings.SwaggerAPIVersion}",
            Version = Settings.SwaggerAPIVersion
        });
    });

    services.AddSwaggerGenNewtonsoftSupport();

    // AutoMapper and Domain
    services.AddAutoMapper(typeof(RequestsProfile), typeof(ResponseProfile));
    services.AddSingleton<StrategyFactory>();
    services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

    // Application Layer
    services.AddScoped<IEntityStrategyFactory, EntityStrategyFactory>();
    services.AddTransient<IApplicationBase<AgentBase<Guid>, Guid>, ApplicationBase<AgentBase<Guid>, Guid>>();
    services.AddTransient<IApplicationBase<EventBase<Guid>, Guid>, ApplicationBase<EventBase<Guid>, Guid>>();
    services.AddTransient<IApplicationBase<QueueBase<Guid>, Guid>, ApplicationBase<QueueBase<Guid>, Guid>>();

    services.AddTransient<IAgentApplication, AgentApplication>();
    services.AddTransient<IAdminApplication, AdminApplication>();
    services.AddTransient<IEventApplication, EventApplication>();
    services.AddTransient<IQueueApplication, QueueApplication>();

    // Services Layer
    services.AddTransient<IServiceBase<AgentBase<Guid>, Guid>, ServiceBase<AgentBase<Guid>, Guid>>();
    services.AddTransient<IServiceBase<EventBase<Guid>, Guid>, ServiceBase<EventBase<Guid>, Guid>>();
    services.AddTransient<IServiceBase<QueueBase<Guid>, Guid>, ServiceBase<QueueBase<Guid>, Guid>>();

    services.AddTransient<IAgentService<AgentBase<Guid>, Guid>, AgentService<AgentBase<Guid>, Guid>>();
    services.AddTransient<IEventService<EventBase<Guid>, Guid>, EventService<EventBase<Guid>, Guid>>();
    services.AddTransient<IQueueService<QueueBase<Guid>, Guid>, QueueService<QueueBase<Guid>, Guid>>();


    if (Settings.UseNoSqlDatabase)
    {
        services.AddSingleton<DocumentDbContext>(sp => new DocumentDbContext(
            Settings.NoSqlDbConnectionString,
            Settings.NoSqlDatabaseName));

        //services.AddTransient(typeof(IRepositoryBase<,>), typeof(DocumentRepositoryBase<,>));
        services.AddTransient<IDataStrategy<AgentBase<string>, string>, DocumentStrategy<AgentBase<string>, string>>();
        services.AddTransient<IDataStrategy<EventBase<string>, string>, DocumentStrategy<EventBase<string>, string>>();
        services.AddTransient<IDataStrategy<QueueBase<string>, string>, DocumentStrategy<QueueBase<string>, string>>();

        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Agent, string>,
            DocumentRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Agent, string>>();
        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Event, string>,
            DocumentRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Event, string>>();
        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Queue, string>,
            DocumentRepositoryBase<CallCenterAgentManager.Domain.Entities.Document.Queue, string>>();
    }
    else
    {
        services.AddDbContext<RelationalDbContext>(options =>
            options.UseNpgsql(Settings.RelationalDbConnectionString));

        //services.AddTransient(typeof(IRepositoryBase<,>), typeof(RelationalRepositoryBase<,>));
        services.AddTransient<IDataStrategy<AgentBase<Guid>, Guid>, RelationalStrategy<AgentBase<Guid>, Guid>>();
        services.AddTransient<IDataStrategy<EventBase<Guid>, Guid>, RelationalStrategy<EventBase<Guid>, Guid>>();
        services.AddTransient<IDataStrategy<QueueBase<Guid>, Guid>, RelationalStrategy<QueueBase<Guid>, Guid>>();

        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Agent, Guid>,
            RelationalRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Agent, Guid>>();
        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Event, Guid>,
            RelationalRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Event, Guid>>();
        services.AddTransient<IRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Queue, Guid>,
            RelationalRepositoryBase<CallCenterAgentManager.Domain.Entities.Relational.Queue, Guid>>();
    }

    services.AddTransient<CallCenterAgentManager.Domain.Repository.Relational.IQueueRepository, CallCenterAgentManager.Data.Repositories.Relational.QueueRepository>();
    services.AddTransient<CallCenterAgentManager.Domain.Repository.Document.IQueueRepository, CallCenterAgentManager.Data.Repositories.Document.QueueRepository>();

}

void ConfigureMiddleware(WebApplication app)
{
    app.UseStaticFiles();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseMiddleware(typeof(ErrorHandlingMiddleware));
    }

    app.UseSwagger();
    app.UseSwaggerUI(configuration =>
    {
        configuration.SwaggerEndpoint($"/swagger/{Settings.SwaggerAPIVersion}/swagger.json", Settings.SwaggerAPIName);
        configuration.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

        configuration.RoutePrefix = string.Empty;
    });

    app.UseCors("AllowAllOrigins");

    app.UseRouting();
    app.MapControllers();
}


void ConfigureSettings(IConfiguration configuration)
{
    Settings.RelationalDbConnectionString = configuration.GetConnectionString("PostgreSqlConnection");
    Settings.NoSqlDbConnectionString = configuration.GetConnectionString("MongoDbConnection");
    Settings.NoSqlDatabaseName = configuration["ConnectionStrings:MongoDbName"];
    Settings.UseNoSqlDatabase = bool.Parse(configuration["DatabaseSettings:UseNoSqlDatabase"]);

    Settings.SwaggerAPIName = configuration["Swagger:APIName"];
    Settings.SwaggerAPIVersion = configuration["Swagger:APIVersion"];
}
