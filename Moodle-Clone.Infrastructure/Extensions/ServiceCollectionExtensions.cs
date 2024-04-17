using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodleClone.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MoodleClone.Domain.Entities;

namespace MoodleClone.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MoodleCloneDb");
        services.AddDbContext<MoodleCloneDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        /*services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MoodleCloneDbContext>();*/

    }
}
