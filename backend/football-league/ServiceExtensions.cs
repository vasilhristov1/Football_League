using football_league.Data.Repositories;
using football_league.Data.Repositories.Abstractions;
using football_league.Managers;
using football_league.Managers.Abstractions;
using football_league.Services.Ranking;
using football_league.Swagger;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace football_league;

public static class ServiceExtensions
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddTransient<ITeamManager, TeamManager>();
        services.AddTransient<IUserManager, UserManager>();
        services.AddTransient<IMatchManager, MatchManager>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ITeamRepository, TeamRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMatchRepository, MatchRepository>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRankingCalculator>(provider =>
        {
            var teamRepo = provider.GetRequiredService<ITeamRepository>();
            var matchRepo = provider.GetRequiredService<IMatchRepository>();
            var basic = new BasicRankingCalculator(teamRepo, matchRepo);
            return new LoggingRankingCalculatorDecorator(basic);
        });

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football League API", Version = "v1" });

            c.OperationFilter<AddHeaderParameterFilter>();

            c.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                {
                    return [api.GroupName];
                }

                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    return [controllerActionDescriptor.ControllerName];
                }

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });

            c.DocInclusionPredicate((name, api) => true);

            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field",
                }
            );

            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                }
            );
            c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
        });

        return services;
    }
}