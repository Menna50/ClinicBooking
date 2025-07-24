using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface IPatientRepo
    {
        public Task<Patient> GetByIdAsync(int id);
        public Task<Patient> GetByUserIdAsync(int userId);
        Task<bool> PatientExistsAsync(int specialtyId);

    }
}
