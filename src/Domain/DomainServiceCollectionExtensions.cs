using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using FluentValidation;
using System.Reflection;

using OeuilDeSauron.Data.Infrastructure;
using OeuilDeSauron.Data.Infrastructure.Repositories;
using OeuilDeSauron.Data.Items;
using OeuilDeSauron.Domain;
using OeuilDeSauron.Domain.Queries;
using OeuilDeSauron.Infrastructure;
using OeuilDeSauron.Data.Identity;
using OeuilDeSauron.Domain.Mapping;
using OeuilDeSauron.Domain.Services;
using OeuilDeSauron.Domain.Behaviours;
using OeuilDeSauron.Data;

namespace OeuilDeSauron;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class DomainServiceCollectionExtensions
{
    /// <summary>
    /// Adds domain to application services.
    /// </summary>
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        // Entity Framework
        services.AddDbContext<MonitoringContext>(options =>
        {
            options
                .UseSqlServer(
                    configuration.GetConnectionString("OeuilDeSauron"),
                    sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                        .CommandTimeout(60))
                .EnableSensitiveDataLogging(environment.IsDevelopment());
            //options.UseTriggers(builder =>
            //{
            //    builder.AddTrigger<GenerateLogin>();
            //    builder.AddTrigger<TransferLogTrigger>();
            //    builder.AddTrigger<UserTrigger>();
            //});
        });

        if (environment.IsDevelopment())
        {
            services.AddDistributedMemoryCache();
        }

        // MediatR
        services.AddMediatR( cfg => cfg.RegisterServicesFromAssembly(typeof(DomainServiceCollectionExtensions).Assembly));

        // AutoMapper
        services.AddAutoMapper(typeof(EntityMapper));
        services.AddFileManager(configuration);

        // Resources
        services.AddTransient<IResources, Resources>();

        // Repositories
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IListRepository, ListRepository>();
        services.AddScoped<IUserLoginRepository, UserLoginRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Queries
        services.AddScoped<IItemQueries, ItemQueries>();
        services.AddScoped<IListQueries, ListQueries>();
        services.AddScoped<IRoleQueries, RoleQueries>();
        services.AddScoped<IUserQueries, UserQueries>();

        // Scoped Services
        services.AddScoped<IUserService, UserService>();

        // Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
