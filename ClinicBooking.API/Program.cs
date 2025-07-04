
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

namespace ClinicBooking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
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
                ) ;


            //RegisterRepos !!
            builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            builder.Services.AddScoped<IGenericRepository<Patient>, GenericRepository<Patient>>();
            builder.Services.AddScoped<IGenericRepository<Doctor>, GenericRepository<Doctor>>();

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            //Register Serivice 
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();



            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });

            //AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AllowNullCollections = true;
            }, AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
             app.UseExceptionHandlinMiddleware();
      //      app.UseMiddleware<MennaExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
