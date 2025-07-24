using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface ISpecialtyRepo
    {
        Task<bool> SpecialtyExistsAsync(int specialtyId);

        Task<IEnumerable<Specialty>> GetAll();
    }
}
