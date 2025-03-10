using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using UserBaseAPI.Application.Core.Context;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Application.Core.Services;
using UserBaseAPI.Application.Extensions;
using UserBaseAPI.Infrastructure;
using UserBaseAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<IUserAPIEFCoreDBContext, UserAPIEFCoreDBContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("UserDBConnectionString"));
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddHttpContextAccessor();
 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
 

builder.Services.AddAuthorization(

    options =>
     
      options.AddPolicy("anypolyceuwant", policy =>
        policy.RequireClaim("email_verified", "true")
    )
      );

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/serilogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();

}

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.Run();
