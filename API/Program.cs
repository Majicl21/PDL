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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DataContext>();

builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IGenericBLL<Utilisateur>, GenericBLL<Utilisateur>>();
builder.Services.AddScoped<IRepository<Utilisateur>, UtilisateurRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add this before Authorization

app.UseAuthorization();

app.MapControllers();

app.Run();
