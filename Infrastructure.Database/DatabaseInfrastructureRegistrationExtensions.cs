using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi;

namespace Infrastructure.Database;

public static class DatabaseInfrastructureRegistrationExtensions
{
    public static IServiceCollection AddDatabaseInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        // Register the current project's dependencies
        services.Scan(scanner => scanner.FromAssemblies(typeof(DatabaseInfrastructureRegistrationExtensions).Assembly)
            .AddClasses(c => c.Where(type => !type.IsNested), publicOnly: false)
            .AsSelfWithInterfaces().WithSingletonLifetime());

        //Add the DbContext
        services.AddDbContextFactory<OrderApiDbContext>(
        options => options.UseSqlServer(configuration["ConnectionStrings:OrderApi"]));

        return services;
    }
}