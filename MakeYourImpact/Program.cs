using MakeYourImpact.Config;
using MakeYourImpact.Infrastructure;
using MakeYourImpact.Infrastructure.Repositories;
using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Make Your Impact",
        Version = "v1"
    });
});

// Add options
builder.Services.Configure<MongoDbConfig>(
    builder.Configuration.GetSection(MongoDbConfig.ConfigurationSection));

// Register the VolonteerDbContext
builder.Services.AddSingleton<VolonteerDbContext>();

// Register repositories
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IUserApplicationsRepository, UserApplicationsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();