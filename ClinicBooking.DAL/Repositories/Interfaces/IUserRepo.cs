using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    
    public interface IUserRepo
    {
    
        public Task<User> GetUserByNameAsync(string name);
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<bool> UserExistByNameAsync(string name);
        public Task<bool> UserExistByEmailAsync(string name);
    }
}
