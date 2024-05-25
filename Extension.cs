using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QRCodeAttendance.Application.Attendace;
using QRCodeAttendance.Application.Auth;
using QRCodeAttendance.Application.Cloud;
using QRCodeAttendance.Application.Company;
using QRCodeAttendance.Application.Dashboard;
using QRCodeAttendance.Application.Department;
using QRCodeAttendance.Application.Email;
using QRCodeAttendance.Application.File;
using QRCodeAttendance.Application.Notification;
using QRCodeAttendance.Application.Position;
using QRCodeAttendance.Application.Token;
using QRCodeAttendance.Application.User;
using QRCodeAttendance.Infrastructure.Constant;
using QRCodeAttendance.Infrastructure.Data;
using QRCodeAttendance.Presentation.Controllers;

namespace QRCodeAttendance;
public static class Extension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql("Host=dpg-comf5akf7o1s73f33sdg-a.singapore-postgres.render.com:5432;Database=qrcodeattendance;Username=qrcodeattendance;Password=Kzogf2p1gfliCrrQ1O4cffdgaQ0bphnf");
        });

        ConfigKey config = new(configuration);
        string? secretKey = configuration["JWT:Key"];
        byte[] secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "qrcodeattendance", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddCors(options =>
        {
            options.AddPolicy("http", builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                builder.WithOrigins("https://thaibaoattendance-latest.vercel.app")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IAttendaceService, AttendaceService>();
        services.AddScoped<INotificationService, NotificationService>();

    }
}
