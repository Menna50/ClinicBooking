using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class AdminDoctorListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SpecialtyName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
