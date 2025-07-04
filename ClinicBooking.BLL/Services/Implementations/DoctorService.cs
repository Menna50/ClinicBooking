using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
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
        private readonly IGenericRepository<Doctor> _repo;
        private readonly IAuthService _authService;
        public DoctorService(IGenericRepository<Doctor> repo, IAuthService authService)
        {
            _repo = repo;
            _authService = authService;
        }
        public async Task AddDoctorAsync(DoctorRegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                throw new Exception("Confirm Passwor is not correct");
            }
            RegisterRequestDto RegisterRequestDto = new RegisterRequestDto()
            {
                UserName = dto.Name,
                Password = dto.Password,
                Email = dto.Email,
                PhoneNumber=dto.PhoneNumber,
                Role = Role.Doctor


            };
            await _authService.RegisterAsync(RegisterRequestDto);
            var doctor = new Doctor()
            {
                Name = dto.Name,
                SpecialtyId = dto.SpecialtyId,
                Bio = dto.Bio,
            
                ConsultationFee = dto.ConsultationFee,
                UserId = 2
            };
            await _repo.AddAsync(doctor);
            //  await    _repo.SaveChangesAsync();
        }
    }
}
