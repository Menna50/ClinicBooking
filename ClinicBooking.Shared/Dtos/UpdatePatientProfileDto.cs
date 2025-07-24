using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class UpdatePatientProfileDto
    {
        public string FName {  get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
