using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Implemetations
{
    public class PatientRepo : IPatientRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Patient> _set;
        public PatientRepo(AppDbContext context)
        {
            _context= context;
            _set=  _context.Set<Patient>();
        }
        public Task<Patient> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByUserIdAsync(int userId)
        {
            return await _set.Include(x=>x.User).FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<bool> PatientExistsAsync(int specialtyId)
        {
            return await _set.AnyAsync(x => x.Id == specialtyId);
        }

       
    }
}
