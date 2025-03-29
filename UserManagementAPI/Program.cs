using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagementAPI.Auth;
using UserManagementAPI.Configuration;
using UserManagementAPI.Models;
using UserManagementAPI.Repository;
using UserManagementAPI.Repository.Interface;
using UserManagementAPI.Services;
using UserManagementAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbConnection, SqliteConnection>(serviceProvider =>
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            return connection;
        });

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connection = serviceProvider.GetRequiredService<DbConnection>();
    options.UseSqlite(connection);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "My API",
            Version = "v1"
        });

        c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic Authentication"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    new string[] {}
                }
        });
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
        c.RoutePrefix = string.Empty;
    });

    app.UseMiddleware<BasicAuth>();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.OpenConnection();
    dbContext.Database.EnsureCreated();

    dbContext.Users.Add(new User { UserId = 1, UserName = "admin@gmail.com", FirstName = "Doco", LastName = "Soft", DOB = DateTime.Today, Address = "Dublin, Ireland" });
    dbContext.SaveChanges();
}

app.MapControllers();
app.Run();