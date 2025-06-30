using AutoMapper;
using ClinicBooking.BLL.Services.Implementations;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Mapping
{
    internal class MappingProfile:Profile
    {
       
        public MappingProfile()
        {
            //     CreateMap<User, UserLoginDto>().ReverseMap();
      //      CreateMap<UserRegisterDto, User>()
      //.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
    //  .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            //   CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
