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
            CreateMap<AddAvailabilityRequestDto, Availability>();
            CreateMap<Availability, AvailabilityListDto>();
            CreateMap<Availability, AvailabilityDto>();
            CreateMap<UpdateAvailabilityRequestDto, Availability>();
            CreateMap<Specialty, SpecialtyDto>();
            CreateMap<Doctor, AdminDoctorListDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty.Name));

            CreateMap<Doctor, AdminDoctorDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty.Name));

            //     CreateMap<User, LoginRequestDto>().ReverseMap();
            //      CreateMap<RegisterRequestDto, User>()
            //.ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //  .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            //   CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
