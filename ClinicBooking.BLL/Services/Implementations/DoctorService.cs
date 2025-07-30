using AutoMapper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _genericRepo;
        private readonly IAuthService _authService;
        private readonly IDoctorRepo _doctorRepo;
        private readonly AppDbContext _dbContext;
        private readonly ISpecialtyRepo _specialtyRepo;
        private readonly IMapper _mapper;

        public DoctorService(IMapper mapper, ISpecialtyRepo specialtyRepo, IGenericRepository<Doctor> repo, IAuthService authService, IDoctorRepo doctorRepo, AppDbContext dbConetxt)
        {
            _genericRepo = repo;
            _authService = authService;
            _doctorRepo = doctorRepo;
            _dbContext = dbConetxt;
            _specialtyRepo = specialtyRepo;
            _mapper = mapper;
        }
        private async Task<ResultT<AuthResponseDto>> RegisterDoctorAsUser(DoctorRegisterDto dto)
        {
            var registerRequest = new RegisterRequestDto
            {
                UserName = dto.UserName,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = Roles.Doctor
            };

            var authResponse = await _authService.RegisterAsync(registerRequest);


            return authResponse;

        }
        public async Task<ResultT<DoctorProfileDto>> AddDoctorAsync(DoctorRegisterDto dto)
        {
            try
            {
                // Start transaction from generic repo
                await _genericRepo.BeginTransactionAsync();

                // Register user for doctor (auth service)
                var authResponse = await RegisterDoctorAsUser(dto);
                if (!authResponse.IsSuccess)
                {
                    await _genericRepo.RollbackTransactionAsync();
                    return ResultT<DoctorProfileDto>.Failure(authResponse.StatusCode, authResponse.Error);
                }

                // 🩺 Create doctor
                var doctor = new Doctor
                {
                    Name = dto.Name,
                    SpecialtyId = dto.SpecialtyId,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    UserId = authResponse.Data.Id
                };

                await _genericRepo.AddAsync(doctor);
                var saved = await _genericRepo.SaveChangesAsync();

                if (!saved)
                {
                    await _genericRepo.RollbackTransactionAsync();
                    return ResultT<DoctorProfileDto>.Failure(StatusCodes.Status500InternalServerError,
                        new Error("SaveFailed", "Failed to save doctor"));
                }

                // Commit transaction
                await _genericRepo.CommitTransactionAsync();

                // Load full doctor details
                var doctorWithDetails = await _doctorRepo.GetByIdAsync(doctor.Id);
                if (doctorWithDetails == null || doctorWithDetails.User == null || doctorWithDetails.Specialty == null)
                {
                    return ResultT<DoctorProfileDto>.Failure(StatusCodes.Status500InternalServerError,
                        new Error("DoctorLoadFailed", "Could not load doctor details after creation."));
                }

                // 🧾 Map to DTO
                var doctorProfileDto = new DoctorProfileDto
                {
                    Id = doctorWithDetails.Id,
                    Name = doctorWithDetails.Name,
                    Email = doctorWithDetails.User.Email,
                    PhoneNumber = doctorWithDetails.User.PhoneNumber,
                    SpecialtyName = doctorWithDetails.Specialty.Name,
                    ConsultationFee = doctorWithDetails.ConsultationFee,
                    Bio = doctorWithDetails.Bio
                };

                return ResultT<DoctorProfileDto>.Success(StatusCodes.Status201Created, doctorProfileDto);
            }
            catch (Exception)
            {
                await _genericRepo.RollbackTransactionAsync();

                return ResultT<DoctorProfileDto>.Failure(StatusCodes.Status500InternalServerError,
                    new Error("DoctorRegistrationFailed", "An error occurred while registering the doctor"));
            }
        }


        public async Task<ResultT<DoctorProfileDto>> GetDoctorProfileAsync(int userId)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor == null)
                return ResultT<DoctorProfileDto>.Failure(404, new Error("Doctor not found", "Doctor not found"));

            var doctorProfileDto = new DoctorProfileDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                ConsultationFee = doctor.ConsultationFee,
                Email = doctor.User.Email,
                Bio = doctor.Bio,
                PhoneNumber = doctor.User.Email,
                SpecialtyName = doctor.Specialty.Name

            };
            return ResultT<DoctorProfileDto>.Success(200, doctorProfileDto);
        }

        public async Task<Result> UpdateDoctorProfile(UpdateDoctorProfileDto dto, int id)
        {
            var doctor = await _doctorRepo.GetByUserIdAsync(id);
            if (doctor == null)
                return Result.Failure(StatusCodes.Status404NotFound, new Error("DoctorNotFound", "Doctor not found"));
            doctor.Name = dto.Name;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.Bio = dto.Bio;
            doctor.User.PhoneNumber = dto.PhoneNumber;
            // not very important coz ef trackes the the doctor which is retrived from dbset in dbcontext
            //  await _genericRepo.UpdateAsync(doctor);
            await _genericRepo.SaveChangesAsync();
            return Result.Success(204);

        }

        public async Task<ResultT<List<DoctorProfileDto>>> GetAllDoctorBySpecialtyIdAsync(int? specialtyId)
        {
            IEnumerable<Doctor> doctors = new List<Doctor>();

            if (specialtyId != null)
            {

                if (!await _specialtyRepo.SpecialtyExistsAsync((int)specialtyId))
                {
                    return ResultT<List<DoctorProfileDto>>.Failure(StatusCodes.Status404NotFound,
                        new Error("SpecialtyIdIsNotExist", "Specialty id is not exixt")
                        );

                }
                doctors = await _doctorRepo.GetAllBySpecialtiyId((int)specialtyId);
            }
            else
                doctors = await _genericRepo.GetAllAsync();

            var doctorDtos = doctors.Select(d => new DoctorProfileDto
            {
                Id = d.Id,
                Name = d.Name,        
                Email = d.User?.Email,
                PhoneNumber = d.User?.PhoneNumber,
                Bio = d.Bio,
                SpecialtyName = d.Specialty?.Name,   
                ConsultationFee = d.ConsultationFee
            }).ToList();


            return ResultT<List<DoctorProfileDto>>.Success(StatusCodes.Status200OK, doctorDtos);
        }
        public async Task<Result> DeleteDoctorAsync(int id)
        {
            var deleted = await _doctorRepo.SoftDeleteAsync(id);

            if (!deleted)
            {
                return Result.Failure(StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found or already deleted"));
            }

            return Result.Success(StatusCodes.Status200OK);
        }
        public async Task<ResultT<List<AdminDoctorListDto>>> GetAllDoctorsAsync(bool includeDeleted = false)
        {
            var doctors = await _doctorRepo.GetAllAsync(includeDeleted);

            var dtos = _mapper.Map<List<AdminDoctorListDto>>(doctors);
            return ResultT<List<AdminDoctorListDto>>.Success(StatusCodes.Status200OK, dtos);
        }
        public async Task<ResultT<AdminDoctorDto>> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id, true);
            if (doctor == null)
                return ResultT<AdminDoctorDto>.Failure(StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found"));

            var dto = _mapper.Map<AdminDoctorDto>(doctor);
            return ResultT<AdminDoctorDto>.Success(StatusCodes.Status200OK, dto);
        }
    }
}
