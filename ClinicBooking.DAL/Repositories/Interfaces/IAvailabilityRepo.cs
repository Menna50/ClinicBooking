using ClinicBooking.DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Repositories.Interfaces
{
    public interface IAvailabilityRepo
    {
        public Task<List<Availability>> GetAllByDoctorIdAsync(int doctorId); 
        public Task<Availability> GetByIdAndDoctorIdAsync(int availabilityId, int doctorId);
        Task<bool> IsOverlappingAsync(
                  int doctorId,
                  DayOfWeek day,
                  TimeOnly startTime,
                  TimeOnly endTime,
                  DateTime effectiveFrom, 
                  DateTime effectiveTo,   
                  int? excludeId = null);
        Task<List<Availability>> GetDoctorAvailabilitiesAsync(int doctorId, DateTime fromDate, DateTime toDate);
        Task<List<DateTime>> GetDoctorBookedAppointmentsAsync(int doctorId, DateTime fromDate, DateTime toDate);
        Task<bool> AvailabilityExistsAsync(int availabilityId);
        Task<Availability?> GetDoctorAvailabilityForSlotAsync(int doctorId, DateTime appointmentDate);


    }
}
