using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class DoctorRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string PhoneNumber { get; set; }
        public int SpecialtyId { get; set; }
      
    }
}
