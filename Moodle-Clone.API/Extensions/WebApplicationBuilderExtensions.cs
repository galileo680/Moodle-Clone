using Microsoft.OpenApi.Models;
using MoodleClone.API.Middlewares;
using MoodleClone.Application.Jobs;
using MoodleClone.Application.Services;
using MoodleClone.Domain.Interfaces;
using Quartz;


namespace MoodleClone.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }
    });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            //Quartz
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("NotifyTeachersJob");
                q.AddJob<NotifyTeachersJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("NotifyTeachersJob-trigger")
                    .WithCronSchedule("0 0 0 * * ?")); // Codziennie o północy
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


        }
    }
}
