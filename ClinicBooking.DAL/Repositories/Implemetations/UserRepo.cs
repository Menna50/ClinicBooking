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

       


        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _users.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> UserExistByNameAsync(string name)
        {
            var t= await _users.AnyAsync(x => x.UserName == name);
            return t;
        }

        public async Task<bool> UserExistByEmailAsync(string email)
        {
             var t = await _users.AnyAsync(x => x.Email == email);
            return t;
        }

    }
}
