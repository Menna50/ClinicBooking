
using ClinicBooking.API.Controllers;
using ClinicBooking.API.Helpers;
using ClinicBooking.BLL.Services.Implementations;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;

using ClinicBooking.API;
using Microsoft.EntityFrameworkCore;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.DAL.Repositories.Implemetations;
using ClinicBooking.API.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClinicBooking.API.Middlewares;
using Microsoft.OpenApi.Models;
using FluentValidation;
using ClinicBooking.Shared.Validators;
using Microsoft.AspNetCore.Identity;
using ClinicBooking.API.Filters;
using ClinicBooking.Shared.Dtos;
using Serilog;

namespace ClinicBooking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration) // ✅ هنا المفتاح!
.CreateLogger();
            // 🟢 ربط Serilog مع ASP.NET Core
            builder.Host.UseSerilog();

            // Add services to the container.

            //Global Filters

            builder.Services.AddControllers(opt=>
            opt.Filters.Add<CustomFluentValidationFilter>()
            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        

            //AddDbContext
            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //AddAuthSerice
            var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSettings>();
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddScoped<ITokenHelper, JwtTokenHelper>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication().AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
                }
                );


            //RegisterRepos !!
            builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            builder.Services.AddScoped<IGenericRepository<Specialty>, GenericRepository<Specialty>>();
            builder.Services.AddScoped<IGenericRepository<Patient>, GenericRepository<Patient>>();
            builder.Services.AddScoped<IGenericRepository<Doctor>, GenericRepository<Doctor>>();
            builder.Services.AddScoped<IGenericRepository<Appointment>, GenericRepository<Appointment>>();

            builder.Services.AddScoped<IGenericRepository<Availability>, GenericRepository<Availability>>();
            //specificRepos
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IPatientRepo,PatientRepo>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IAvailabilityRepo, AvailabilityRepo>();
            builder.Services.AddScoped<ISpecialtyRepo, SpecialtyRepo>();
            //Register Serivice 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();




            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });

            //FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(DoctorRegisterDtoValidator).Assembly);
         //   builder.Services.AddValidatorsFromAssembly(typeof(RegisterRequestDto).Assembly);

            //AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AllowNullCollections = true;
            }, AppDomain.CurrentDomain.GetAssemblies());


            builder.Services.AddSwaggerGen(options =>
            {
                // Define the security scheme for JWT Bearer Token
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", // The name of the header
                    In = ParameterLocation.Header, // Where the token is located (in the header)
                    Type = SecuritySchemeType.ApiKey, // Type of security scheme (API Key, HTTP, OAuth2, OpenIdConnect)
                    Scheme = "Bearer" // The name of the authentication scheme (e.g., Bearer, Basic)
                });

                // Add a security requirement to apply the Bearer scheme to all operations
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                   {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Refers to the "Bearer" security definition above
                }
            },
            new string[] { }
        }
    });

            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandlingMiddleware();
            //      app.UseMiddleware<MennaExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
