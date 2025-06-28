using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IGenericService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsync(int id);
        public Task AddAsync(T item);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(T item);
    }
  
 
}
