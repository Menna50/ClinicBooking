﻿using ClinicBooking.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task AddDoctorAsync(DoctorRegisterDto doctorRegisterDto);
    }
}
