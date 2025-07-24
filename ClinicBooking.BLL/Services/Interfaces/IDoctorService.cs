using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<ResultT<DoctorProfileDto>> AddDoctorAsync(DoctorRegisterDto doctorRegisterDto);
        public Task<ResultT<DoctorProfileDto>> GetDoctorProfileAsync(int id);
        public Task<Result> UpdateDoctorProfile(UpdateDoctorProfileDto dto, int id);
        public Task<ResultT<List<DoctorProfileDto>>> GetAllDoctorBySpecialtyIdAsync(int specialtyId);   
    }
}
