using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo= userRepo;
            
        }
        public async Task< User >GetUserByAsync(Func<User, bool> predicate)
        {
            return await _userRepo.GetUserByAsync(predicate);
        }

        public async Task< IEnumerable<User>> GetUsersByAsync(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
