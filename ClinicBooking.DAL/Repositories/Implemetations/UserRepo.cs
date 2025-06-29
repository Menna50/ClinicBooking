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
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _users;
        public UserRepo(AppDbContext context)
        {
            _context = context;
            _users =_context.Set<User>();
        }

        public async Task AddUser(User user)
        {
           await _users.AddAsync(user);
            _context.SaveChanges();
            
        }

        public async Task< User> GetUserByAsync(Func<User,bool> predicate)
        {
            return _users.FirstOrDefault(predicate);
        }

       

        public async Task< IEnumerable<User>> GetUsersByAsync(Func<User,bool> predicate)
        {
            return _users.Where(predicate);
        }
    }
}
