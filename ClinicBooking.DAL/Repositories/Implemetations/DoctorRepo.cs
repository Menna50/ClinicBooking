using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Implemetations
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Doctor> _set;  
        public DoctorRepo(AppDbContext context)
        {
            _context= context;
            _set=_context.Set<Doctor>();
        }

        public async Task<bool> DoctorExistsAsync(int doctorId)
        {
            return await _set.AnyAsync(x => x.Id == doctorId);

        }

        public async Task<IEnumerable<Doctor>> GetAllBySpecialtiyId(int specialtyId)
        {
            return  _set
                .Include(x=>x.User)
                .Include(x=>x.Specialty)
                .Where(x => x.SpecialtyId == specialtyId);
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _set
                .Include(d => d.User)
                .Include(d=>d.Specialty)
                .FirstOrDefaultAsync(d=>d.Id==id);
            return doctor;
        }
        public async Task<Doctor> GetByUserIdAsync(int id)
        {
            var doctor = await _set
               .Include(d => d.User)
               .Include(d => d.Specialty)
               .FirstOrDefaultAsync(d => d.UserId == id);
            return doctor;
        }
        public async Task<bool> SoftDeleteAsync(int doctorId)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == doctorId && !d.IsDeleted);

            if (doctor == null)
                return false;

            doctor.IsDeleted = true;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(bool includeDeleted = false)
        {
            IQueryable<Doctor> query = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialty);

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }
        public async Task<Doctor?> GetByIdAsync(int id, bool includeDeleted = false)
        {
            IQueryable<Doctor> query = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Specialty);

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync(d => d.Id == id);
        }

    }
}
