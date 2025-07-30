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
    public class AvailabilityRepo : IAvailabilityRepo
    {
        private readonly AppDbContext _context;
      private readonly DbSet<Availability> _dbSet;
        public AvailabilityRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Availability>();
        }

        public async Task<List<Availability>> GetAllByDoctorIdAsync(int doctorId)
        {
            return await _dbSet.Where(x=>x.DoctorId==doctorId).ToListAsync();
        }

        public async Task<Availability> GetByIdAndDoctorIdAsync(int availabilityId, int doctorId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == availabilityId && x.DoctorId == doctorId);
        }
        public async Task<bool> IsOverlappingAsync(
                   int doctorId,
                   DayOfWeek day,
                   TimeOnly startTime,
                   TimeOnly endTime,
                   DateTime effectiveFrom, 
                   DateTime effectiveTo,   
                   int? excludeId = null)
        {
            var query = _dbSet 
                .Where(a => a.DoctorId == doctorId && a.Day == day);

            if (excludeId.HasValue)
            {
                query = query.Where(a => a.Id != excludeId.Value);
            }

            // The core overlap logic:
            // 1. Time overlap: (newStart < existingEnd AND newEnd > existingStart)
            // 2. Date overlap: (newFrom <= existingTo AND newTo >= existingFrom)
            //    Note: We use .Date to compare only the date part, ignoring time components for date range overlap.
            //    We also handle existing.EffectiveFrom/To being nullable by using ?? DateTime.MinValue/MaxValue.

            var isOverlapping = await query.AnyAsync(existing =>
                // Time overlap
                (startTime < existing.EndTime && endTime > existing.StartTime) &&
                // Date overlap (handle nullable existing dates)
                (
                    (effectiveFrom.Date <= (existing.EffectiveTo ?? DateTime.MaxValue).Date) &&
                    (effectiveTo.Date >= (existing.EffectiveFrom ?? DateTime.MinValue).Date)
                )
            );

            return isOverlapping;
        }



        public async Task<List<Availability>> GetDoctorAvailabilitiesAsync(int doctorId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Availabilities
                .Where(a => a.DoctorId == doctorId &&
                            a.EffectiveFrom <= toDate &&
                            a.EffectiveTo >= fromDate)
                .ToListAsync();
        }

        public async Task<List<DateTime>> GetDoctorBookedAppointmentsAsync(int doctorId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                            a.AppointmentDate.Date >= fromDate.Date &&
                            a.AppointmentDate.Date <= toDate.Date
                            && (a.Status == AppointmentStatus.Scheduled ||
                     a.Status == AppointmentStatus.Completed ||
                     a.Status == AppointmentStatus.NoShow))
                .Select(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<bool> AvailabilityExistsAsync(int availabilityId)
        {
            return await _dbSet.AnyAsync(x => x.Id == availabilityId);
        }
        public async Task<Availability?> GetDoctorAvailabilityForSlotAsync(int doctorId, DateTime appointmentDate)
        {
            var date = appointmentDate.Date;
            var time = appointmentDate.TimeOfDay;
            var dayOfWeek = appointmentDate.DayOfWeek;

            // Step 1: فلترة البيانات القابلة للترجمة (SQL only)
            var availabilities = await _context.Availabilities
                .Where(a => a.DoctorId == doctorId &&
                            a.Day == dayOfWeek &&
                            a.EffectiveFrom <= date &&
                            a.EffectiveTo >= date)
                .ToListAsync(); // 👈 التحويل من LINQ to Entities إلى LINQ to Objects

            // Step 2: فلترة الوقت بالـ C#
            return availabilities
                .FirstOrDefault(a =>
                    a.StartTime.ToTimeSpan() <= time &&
                    time + TimeSpan.FromMinutes(a.SlotDurationMinutes) <= a.EndTime.ToTimeSpan());
        }
    }

}
