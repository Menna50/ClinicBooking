using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface IAppointmentRepo
    {
        Task<bool> IsSlotBookedAsync(int doctorId, DateTime appointmentDate);
        Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId, bool upcomingOnly);
        Task<List<Appointment>> GetAppointmentsByPatientAsync(int patientId, bool upcomingOnly);
        Task<Appointment?> GetByIdAsync(int appointmentId);
    }
}
