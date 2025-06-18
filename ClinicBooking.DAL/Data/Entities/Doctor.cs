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
        public string Specialty { get; set; }
        public string Description { get; set; }
        public int UserId { set; get; }
        public User User { set; get; }

        public ICollection<Availability> Availabilities { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

}
