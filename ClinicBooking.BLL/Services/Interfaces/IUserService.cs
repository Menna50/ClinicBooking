using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IUserService
    {
      public Task< User> GetUserByAsync(Func<User, bool> predicate);
        public Task< IEnumerable<User>> GetUsersByAsync(Func<User, bool> predicate);
    }
}
