using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IPatientService
    {
        public Task <ResultT<PatientProfileDto>> AddAsync(PatientRegisterRequestDto dto);
        public Task<Result> UpdateAsync(UpdatePatientProfileDto dto,int userId);

        public Task<ResultT<PatientProfileDto>> GetProfileAsync(int userId);
    }
}
