using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClinicBooking.API.Helpers;
using ClinicBooking.BLL.Helper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class UserTest
    {
        public int Id { get; set; }
    }
    public class AuthService : IAuthService

    {

        private readonly IUserRepo _userRepo;
        private readonly ITokenHelper _jwtHelper;
        private readonly IMapper _mapper;

      
        public AuthService(IUserRepo userRepo, ITokenHelper jwtHelper,IMapper mapper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
           
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
            var user = new User()
            {
                UserName = dto.UserName,
                PasswordHash = passwordAndSalt.hashedPassword,
                PasswordSalt = passwordAndSalt.salt,
                Role = dto.Role
            };

            _userRepo.AddUser(user);

            return "Done";

        }

       

       
    }
}
