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
        public Task< User> GetUserByAsync(Func<User, bool> predicate);
        public Task< IEnumerable<User>> GetUsersByAsync(Func<User, bool> predicate);
        public Task AddUser(User user);
    }
}
