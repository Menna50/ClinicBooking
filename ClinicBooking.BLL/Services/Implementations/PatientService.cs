using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.MennaRepo;
using ClinicBooking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IGenericRepository<Patient> _repo;
        public PatientService(IGenericRepository<Patient> repo) 
        {
            _repo= repo;
        } 
        public Task AddAsync(Patient item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
          return _repo.GetAllAsync();   
        }

        public Task<Patient> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Patient item)
        {
            throw new NotImplementedException();
        }
    }
}
