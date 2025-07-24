using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using ClinicBooking.API.Helpers;
using ClinicBooking.BLL.Helper;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.ErrorCodes;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace ClinicBooking.BLL.Services.Implementations
{
  
    public class AuthService : IAuthService

    {

        private readonly IUserRepo _userRepo;
        private readonly ITokenHelper _jwtHelper;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _genericRepo;


        public AuthService(IGenericRepository<User> genericRepo,IUserRepo userRepo, ITokenHelper jwtHelper, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
            _mapper = mapper;

        }
        private async Task<User?> FindUserByIdentifierAsync(string identifier)
        {
            return identifier.Contains("@")
                ? await _userRepo.GetUserByEmailAsync(identifier)
                : await _userRepo.GetUserByNameAsync(identifier);
        }
        public async Task<ResultT<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            User? user = await FindUserByIdentifierAsync(request.Identifier);


            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return ResultT<AuthResponseDto>.Failure(StatusCodes.Status400BadRequest,
                    new Error(AuthErrorCodes.IncorrectLoginData,"Login data is incorrect"));
            }

            var tokenAndExpiresAt = _jwtHelper.GenerateToken(new JwtUserModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role
            });
            var authResponseDto = new AuthResponseDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = tokenAndExpiresAt.token,
                ExpiresAt = tokenAndExpiresAt.expriesAt

            };
             return ResultT<AuthResponseDto>.Success(StatusCodes.Status200OK,authResponseDto);

        }

        public async Task<ResultT<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto)
        {
           
              var passwordHashAndSalt = PasswordHasher.HashPassword(dto.Password);
            var user = new User()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber=dto.PhoneNumber,
                PasswordHash = passwordHashAndSalt.hashedPassword,
                PasswordSalt = passwordHashAndSalt.salt,
                Role = dto.Role
            };
            await _genericRepo.AddAsync(user);
         await   _genericRepo.SaveChangesAsync();
            var tokenAndExpiresAt = _jwtHelper.GenerateToken(new JwtUserModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role
            });
            var authResponseDto = new AuthResponseDto()
            {
                Id = user.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                Role = dto.Role.ToString(),
                Token = tokenAndExpiresAt.token,
                ExpiresAt = tokenAndExpiresAt.expriesAt

            };
            return ResultT<AuthResponseDto>.Success(StatusCodes.Status201Created, authResponseDto);



        }




    }
}
