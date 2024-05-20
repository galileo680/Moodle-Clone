using MoodleClone.API.Extensions;
using MoodleClone.API.Middlewares;
using MoodleClone.Application.Extensions;
using MoodleClone.Domain.Entities;
using MoodleClone.Infrastructure.Extensions;
using MoodleClone.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.AddPresentation();
builder.Services.AddAplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
        //.AllowCredentials();
    });
});

var app = builder.Build();

/*var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRepositorySeeder>();

await seeder.Seed();*/

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("MyCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();*/
app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
