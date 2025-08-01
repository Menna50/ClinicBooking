﻿using ClinicBooking.DAL.Data.Enums;

namespace ClinicBooking.Shared.Dtos
{
    public class JwtUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
