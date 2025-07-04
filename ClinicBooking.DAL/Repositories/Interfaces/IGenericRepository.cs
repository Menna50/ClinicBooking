using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {

        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task AddAsync(T entity);
        public  Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<bool> SaveChangesAsync();


    }
}
