using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public string Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public int UserId { set; get; }
        public User User { set; get; }

        public ICollection<Availability> Availabilities { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

}
