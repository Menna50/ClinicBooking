using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class CreateAppointmentRequestDto
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

    }
}
