using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Implemetations;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IAuthService _authService;
        private readonly IGenericRepository<Patient> _genericRepo;
        private readonly AppDbContext _dbContext;
        private readonly IPatientRepo _patientRepo;

        public PatientService(IPatientRepo patientRepo, IAuthService authService, IGenericRepository<Patient> genericRepository, AppDbContext dbContext)
        {
            _authService = authService;
            _genericRepo = genericRepository;
            _dbContext = dbContext;
            _patientRepo = patientRepo;
        }

        private async Task<ResultT<AuthResponseDto>> RegisterPatientAsUser(PatientRegisterRequestDto dto)
        {
            var registerRequest = new RegisterRequestDto
            {
                UserName = dto.UserName,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = Roles.Patient
            };

            var authResponse = await _authService.RegisterAsync(registerRequest);


            return authResponse;

        }
        public async Task<ResultT<PatientProfileDto>> AddAsync(PatientRegisterRequestDto dto)
        {
            try
            {
                await _genericRepo.BeginTransactionAsync();

                var authResponse = await RegisterPatientAsUser(dto);

                if (!authResponse.IsSuccess)
                {
                    await _genericRepo.RollbackTransactionAsync();
                    return ResultT<PatientProfileDto>.Failure(authResponse.StatusCode, authResponse.Error);
                }

                var patient = new Patient
                {
                    FName = dto.FName,
                    LName = dto.LName,
                    Gender = dto.Gender,
                    Age = dto.Age,
                    UserId = authResponse.Data.Id
                };

                await _genericRepo.AddAsync(patient);
                var saved = await _genericRepo.SaveChangesAsync();

                //if (!saved)
                //{
                //    await _genericRepo.RollbackTransactionAsync();
                //    return ResultT<PatientProfileDto>.Failure(StatusCodes.Status500InternalServerError,
                //        new Error("SaveFailed", "Failed to save patient"));
                //}

                await _genericRepo.CommitTransactionAsync();

                var patientProfileDto = new PatientProfileDto
                {
                    FName = dto.FName,
                    LName = dto.LName,
                    Gender = dto.Gender,
                    Age = dto.Age,
                    UserName = authResponse.Data.UserName,
                    Email = authResponse.Data.Email,
                    PhoneNumber = patient.User.PhoneNumber
                };

                return ResultT<PatientProfileDto>.Success(StatusCodes.Status201Created, patientProfileDto);
            }
            catch (Exception ex)
            {
               
                await _genericRepo.RollbackTransactionAsync();

                return ResultT<PatientProfileDto>.Failure(StatusCodes.Status500InternalServerError,
                    new Error(ex.InnerException.ToString(), "An error occurred while registering the patient"));
            }
        }
        public async Task<ResultT<PatientProfileDto>> GetProfileAsync(int userId)
        {
            var patient = await _patientRepo.GetByUserIdAsync(userId);


            if (patient == null)
                return ResultT<PatientProfileDto>.Failure(StatusCodes.Status404NotFound,
                    new Error("PatientNotFound", "Patient not found"));
            var dto = new PatientProfileDto()
            {
                FName = patient.FName,
                LName = patient.LName,
                Gender = patient.Gender,
                Age = patient.Age,
                Email = patient.User.Email,
                UserName = patient.User.UserName,

                PhoneNumber = patient.User.PhoneNumber
            };

            return ResultT<PatientProfileDto>.Success(200, dto);
        }

        public async Task<Result> UpdateAsync(UpdatePatientProfileDto dto,int userId)
        {
            var patient = await _patientRepo.GetByUserIdAsync(userId);


            if (patient == null)
                return Result.Failure(StatusCodes.Status404NotFound,
                    new Error("PatientNotFound", "Patient not found"));
            patient.FName = dto.FName;
            patient.LName = dto.LName;
            patient.User.PhoneNumber = dto.PhoneNumber;
          await  _genericRepo.SaveChangesAsync();
            return Result.Success(StatusCodes.Status204NoContent);
        }
    }
}
