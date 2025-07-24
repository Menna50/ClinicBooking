using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Repositories.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _genericRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IAvailabilityRepo _availabilityRepo;
        private readonly IAppointmentRepo _appointmentRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo,IGenericRepository<Appointment> genericRepo,
            IPatientRepo patientRepo, IDoctorRepo doctorRepo,
            IAvailabilityRepo availabilityRepo)
        {
            _genericRepo = genericRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _availabilityRepo = availabilityRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<ResultT<string>> BookAppointmentAsync(CreateAppointmentRequestDto dto, int userId)
        {
            var patient = await _patientRepo.GetByUserIdAsync(userId);

            // 1. تأكد إن الدكتور والمريض موجودين
            if (!await _doctorRepo.DoctorExistsAsync(dto.DoctorId))
                return ResultT<string>.Failure(StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound","Doctor not found"));

            if (!await _patientRepo.PatientExistsAsync(patient.Id))
                return ResultT<string>.Failure(StatusCodes.Status404NotFound,
                    new Error("PatientNotFound", "Patient not found"));
            // 2. تحقق من جدول الإتاحة
            var availability = await _availabilityRepo.GetDoctorAvailabilityForSlotAsync(dto.DoctorId, dto.AppointmentDate);
            if (availability == null)
                return ResultT<string>.Failure(StatusCodes.Status400BadRequest, 
                    new Error("DoctorNotAvailableAtThisTime", "Doctor not available at this time"));

            // 3. تحقق إن الـ slot مش محجوزة
            var isBooked = await _appointmentRepo.IsSlotBookedAsync(dto.DoctorId, dto.AppointmentDate);
            if (isBooked)
                return ResultT<string>.Failure(StatusCodes.Status409Conflict,
                    new Error("SlotAlreadyBooked", "Slot already booked"));

            // 4. إنشاء الحجز
            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = patient.Id,
                AppointmentDate = dto.AppointmentDate,
                Status = AppointmentStatus.Scheduled
            };

            await _genericRepo.AddAsync(appointment);
            await _genericRepo.SaveChangesAsync();

            return ResultT<string>.Success(StatusCodes.Status201Created, "Appointment booked successfully");
        }
        public async Task<ResultT<List<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int userId, bool upcomingOnly)
        {
            var doctor =await _doctorRepo.GetByUserIdAsync(userId);
            if (doctor==null)
                return  ResultT<List<AppointmentDto>>.Failure(StatusCodes.Status404NotFound,
                    new Error("DoctorNotFound", "Doctor not found"));
            var appointments = await _appointmentRepo.GetAppointmentsByDoctorAsync(doctor.Id, upcomingOnly);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                AppointmentDate = a.AppointmentDate,
                PatientName = a.Patient?.FName + a.Patient?.LName ?? "N/A",
                DoctorName = a.Doctor?.Name ?? "N/A",
                Status = a.Status
            }).ToList();

            return ResultT<List<AppointmentDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<ResultT<List<AppointmentDto>>> GetAppointmentsByPatientIdAsync(int userId, bool upcomingOnly)
        {
            var patient = await _patientRepo.GetByUserIdAsync(userId);
            if (patient==null)
                return ResultT<List<AppointmentDto>>.Failure(StatusCodes.Status404NotFound,
                    new Error("PatientNotFound", "Patient not found"));

            var appointments = await _appointmentRepo.GetAppointmentsByPatientAsync(patient.Id, upcomingOnly);

            var dtos = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                AppointmentDate = a.AppointmentDate,
                DoctorName = a.Doctor?.Name ?? "N/A",
                PatientName = a.Patient?.FName+a.Patient?.LName ?? "N/A",
                Status = a.Status
            }).ToList();

            return ResultT<List<AppointmentDto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<Result> CancelAppointmentAsync(int appointmentId, int currentUserId, Roles currentUserRole)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
                return Result.Failure(StatusCodes.Status404NotFound,
                    new Error("Appointment not found", "Appointment not found"));

            // ✅ تحقق من الصلاحية
            if (currentUserRole == Roles.Doctor)
            {
                var doctor = await _doctorRepo.GetByUserIdAsync(currentUserId);
                if (doctor == null || appointment.DoctorId != doctor.Id)
                {
                    return Result.Failure(StatusCodes.Status403Forbidden,
                        new Error("Access denied", "You do not have permission to cancel this appointment"));
                }
            }
            else if (currentUserRole == Roles.Patient)
            {
                var patient = await _patientRepo.GetByUserIdAsync(currentUserId);
                if (patient == null || appointment.PatientId != patient.Id)
                {
                    return Result.Failure(StatusCodes.Status403Forbidden,
                        new Error("Access denied", "You do not have permission to cancel this appointment"));
                }
            }
            else if (currentUserRole != Roles.Admin)
            {
                return Result.Failure(StatusCodes.Status403Forbidden,
                    new Error("Access denied", "Only admin, doctor or patient can cancel appointments"));
            }

            // ✅ بعد التحقق من الصلاحية: تأكد من الحالة
            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                return Result.Failure(StatusCodes.Status400BadRequest,
                    new Error("Appointment already cancelled", "The appointment is already cancelled"));
            }

            // ✅ إلغاء الموعد
            appointment.Status = AppointmentStatus.Cancelled;
            await _genericRepo.SaveChangesAsync();

            return Result.Success(StatusCodes.Status200OK);
        }


        public async Task<Result> UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status)
        {
            var appointment = await _genericRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
                return Result.Failure(StatusCodes.Status404NotFound, new Error("Appointment not found", "Appointment not found"));

            appointment.Status = status;
            await _genericRepo.SaveChangesAsync();

            return Result.Success(StatusCodes.Status200OK);
        }
    }
}
