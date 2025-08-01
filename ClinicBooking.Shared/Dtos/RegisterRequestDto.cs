﻿using ClinicBooking.DAL.Data.Enums;

namespace ClinicBooking.Shared.Dtos
{

    public class RegisterRequestDto
    {
       public string UserName {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Roles Role { get; set; }
    }
}
