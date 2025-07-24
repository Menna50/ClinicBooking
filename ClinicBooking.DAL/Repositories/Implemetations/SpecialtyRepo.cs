
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
    public class SpecialtyRepo : ISpecialtyRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Specialty> _set;
        public SpecialtyRepo(AppDbContext context)
        {
            _context = context;   
            _set=_context.Set<Specialty>();
        }

        public async Task<IEnumerable<Specialty>> GetAll()
        {
            return _set;
        }

        public async Task<bool> SpecialtyExistsAsync(int specialtyId)
        {
            return  await _set.AnyAsync(x => x.Id == specialtyId);
        }
    }
}
