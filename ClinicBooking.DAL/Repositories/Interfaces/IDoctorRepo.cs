using ClinicBooking.DAL.Data.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface IDoctorRepo
    {
        public Task<Doctor> GetByIdAsync(int id);
        public Task<Doctor> GetByIdAsync(int id, bool includeDeleted = false);

        public Task<Doctor> GetByUserIdAsync(int id);
       public Task<IEnumerable<Doctor>> GetAllBySpecialtiyId(int specialtyId);
        Task<bool> DoctorExistsAsync(int doctorId);
        Task<bool> SoftDeleteAsync(int doctorId);
        Task<IEnumerable<Doctor>> GetAllAsync(bool includeDeleted = false);


    }
}
