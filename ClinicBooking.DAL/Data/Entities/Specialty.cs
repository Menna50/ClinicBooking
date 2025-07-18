﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Entities
{
    public class Specialty
    {
       public int Id { get; set; }  
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
