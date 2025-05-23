using Project.BLL.Contracts;
using Project.BLL;
using Project.Context;
using Project.DAL;
using Project.DAL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;
using Project.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Project.Services;
using Microsoft.Extensions.Options;
using API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200", "http://127.0.0.1:3000", "http://127.0.0.1:4200", "http://localhost:5198", "http://127.0.0.1:5198")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// Add services to the container.
var Cnx = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(Cnx, b => b.MigrationsAssembly("API"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}                    
);

// Configure JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});


builder.Services.AddControllers();

// Register DataContext
builder.Services.AddScoped<DataContext>();

// Register Repositories
builder.Services.AddScoped<IRepository<Utilisateur>, UtilisateurRepository>();
builder.Services.AddScoped<IRepository<Project.Entities.Project>, ProjectRepository>();
builder.Services.AddScoped<IRepository<Notification>, NotificationRepository>();
builder.Services.AddScoped<IRepository<Timesheet>, TimesheetRepository>();
builder.Services.AddScoped<IRepository<UserProject>, UserProjectRepository>();

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register BLL Services
builder.Services.AddScoped<IGenericBLL<Utilisateur>, GenericBLL<Utilisateur>>();
builder.Services.AddScoped<IGenericBLL<Project.Entities.Project>, GenericBLL<Project.Entities.Project>>();
builder.Services.AddScoped<IGenericBLL<Notification>, GenericBLL<Notification>>();
builder.Services.AddScoped<IGenericBLL<Timesheet>, GenericBLL<Timesheet>>();
builder.Services.AddScoped<IGenericBLL<UserProject>, GenericBLL<UserProject>>();

// Register Application Services
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<IUserProjectService, UserProjectService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS must be before auth and routing middleware
app.UseCors("AllowSpecificOrigins");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
