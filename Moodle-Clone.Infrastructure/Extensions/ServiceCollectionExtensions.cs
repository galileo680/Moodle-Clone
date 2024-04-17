using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moodle_Clone.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moodle_Clone.Domain.Entities;

namespace Moodle_Clone.Infrastructure.Extensions;

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
