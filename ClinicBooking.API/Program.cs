
using ClinicBooking.API.Controllers;
using ClinicBooking.BLL.Services.Implementations;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.MennaRepo;
using ClinicBooking.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

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



            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





            //RegisterRepos !!
            builder.Services.AddScoped<IGenericRepository<User>,GenericRepository<User>>();
            builder.Services.AddScoped<IGenericRepository<Patient>, GenericRepository<Patient>>();
            builder.Services.AddScoped<IGenericRepo<User>,GenericRepo<User>>();
            //Register Serivice 
            builder.Services.AddScoped<IPatientService, PatientService>();


            builder.Services.Configure<MyAppSettings>(
    builder.Configuration.GetSection("MyAppSettings"));


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
            });

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
        }
    }
}
