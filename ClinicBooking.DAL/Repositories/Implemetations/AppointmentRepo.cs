using ClinicBooking.DAL.Data;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Implemetations
{
    public class AppointmentRepo:IAppointmentRepo
    {
        private readonly AppDbContext _context;
        public AppointmentRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsSlotBookedAsync(int doctorId, DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a => a.DoctorId == doctorId &&
                               a.AppointmentDate == appointmentDate &&
                               a.Status != AppointmentStatus.Cancelled);
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorAsync(int doctorId, bool upcomingOnly)
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == doctorId);

            if (upcomingOnly)
                query = query.Where(a => a.AppointmentDate >= DateTime.Now);

            return await query.OrderBy(a => a.AppointmentDate).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientAsync(int patientId, bool upcomingOnly)
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId);

            if (upcomingOnly)
                query = query.Where(a => a.AppointmentDate >= DateTime.Now);

            return await query.OrderBy(a => a.AppointmentDate).ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);
        }
    }
}
