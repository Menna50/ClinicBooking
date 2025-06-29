using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClinicBooking.API.Helpers;
using ClinicBooking.BLL.Helper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class AuthService : IAuthService

    {

        private readonly IUserRepo _userRepo;
        private readonly ITokenHelper _jwtHelper; 
      
        public AuthService(IUserRepo userRepo, ITokenHelper jwtHelper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
           
        }
        public async Task<UserDto> LoginAsync(UserLoginDto dto)
        {
            //Check if user exist
            var user = await _userRepo.GetUserByAsync(u => u.UserName == dto.UserName);
            if (user == null)
                return null;

            if (!PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var token = _jwtHelper.GenerateToken(new JwtUserModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Role = user.Role
            });
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = token
            };
          

        }

        public async Task<string> RegisterAsync(UserRegisterDto dto)
        {
            var passwordAndSalt = PasswordHasher.HashPassword(dto.Password);
            User user = new User()
            {
                UserName = dto.UserName,
                Role = dto.Role,
                PasswordHash = passwordAndSalt.hashedPassword,
                PasswordSalt = passwordAndSalt.salt

            };
            _userRepo.AddUser(user);
            
            return "Done";

        }

       

       
    }
}
