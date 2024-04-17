using MoodleClone.API.Extensions;
using MoodleClone.Domain.Entities;
using MoodleClone.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
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
