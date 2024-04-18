using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodleClone.Infrastructure.Persistence;
using MoodleClone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MoodleClone.Infrastructure.Seeder;


namespace MoodleClone.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MoodleCloneDb");
        services.AddDbContext<MoodleCloneDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MoodleCloneDbContext>();

        services.AddScoped<IRepositorySeeder, RepositorySeeder>();

    }
}
