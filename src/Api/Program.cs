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
using CallCenterAgentManager.Domain.Repository;
using CallCenterAgentManager.Domain.Service;
using CallCenterAgentManager.Domain.Service.Contracts;
using CallCenterAgentManager.Domain.Strategy;
using CallCenterAgentManager.Domain.Strategy.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.Extensions.Hosting;
using CallCenterAgentManager.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Serviços
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuração de Middleware e Rotas
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

    // Database Configuration
    if (Settings.UseNoSqlDatabase)
    {
        services.AddSingleton<DocumentDbContext>(sp => new DocumentDbContext(
            Settings.NoSqlDbConnectionString,
            Settings.NoSqlDatabaseName));
    }
    else
    {
        services.AddDbContext<RelationalDbContext>(options =>
            options.UseNpgsql(Settings.RelationalDbConnectionString));
    }

    // AutoMapper and Domain
    services.AddSingleton<StrategyFactory>();
    services.AddAutoMapper(typeof(RequestsProfile), typeof(ResponseProfile));

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

    // Strategy Layer
    services.AddTransient<IDataStrategy<AgentBase<Guid>, Guid>, RelationalStrategy<AgentBase<Guid>, Guid>>();
    services.AddTransient<IDataStrategy<AgentBase<string>, string>, DocumentStrategy<AgentBase<string>, string>>();

    services.AddTransient<IDataStrategy<EventBase<Guid>, Guid>, RelationalStrategy<EventBase<Guid>, Guid>>();
    services.AddTransient<IDataStrategy<EventBase<string>, string>, DocumentStrategy<EventBase<string>, string>>();

    services.AddTransient<IDataStrategy<QueueBase<Guid>, Guid>, RelationalStrategy<QueueBase<Guid>, Guid>>();
    services.AddTransient<IDataStrategy<QueueBase<string>, string>, DocumentStrategy<QueueBase<string>, string>>();


    // Repository Layer
    services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
    services.AddTransient<IRepositoryBase<AgentBase<Guid>, Guid>, RelationalRepositoryBase<AgentBase<Guid>, Guid>>();
    services.AddTransient<IRepositoryBase<AgentBase<string>, string>, DocumentRepositoryBase<AgentBase<string>, string>>();

    services.AddTransient<IRepositoryBase<EventBase<Guid>, Guid>, RelationalRepositoryBase<EventBase<Guid>, Guid>>();
    services.AddTransient<IRepositoryBase<EventBase<string>, string>, DocumentRepositoryBase<EventBase<string>, string>>();

    services.AddTransient<IRepositoryBase<QueueBase<Guid>, Guid>, RelationalRepositoryBase<QueueBase<Guid>, Guid>>();
    services.AddTransient<IRepositoryBase<QueueBase<string>, string>, DocumentRepositoryBase<QueueBase<string>, string>>();

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
