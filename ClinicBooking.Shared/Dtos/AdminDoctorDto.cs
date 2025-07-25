using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class AdminDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SpecialtyName { get; set; }
        public decimal ConsultationFee { get; set; }
        public string Bio { get; set; }
        public bool IsDeleted { get; set; }
    }

}
