using ClinicBooking.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class PatientProfileDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
