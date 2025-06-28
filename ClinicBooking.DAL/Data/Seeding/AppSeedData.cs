using System;
using System.Collections.Generic;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.DAL.Data.Entities;

namespace ClinicBooking.DAL.Seeding
{
    public static class AppSeedData
    {
        public static IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Username = "admin", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Role.Admin },
                new User { Id = 2, Username = "doctor1", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Role.Doctor },
                new User { Id = 3, Username = "doctor2", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Role.Doctor },
                new User { Id = 4, Username = "patient1", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Role.Patient },
                new User { Id = 5, Username = "patient2", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Role.Patient }
            };
        }

        public static IEnumerable<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor { Id = 1, Name = "Dr. Ahmed", Specialty = "Cardiology", Description = "Senior Doctor", UserId = 2 },
                new Doctor { Id = 2, Name = "Dr. Mona", Specialty = "Dermatology", Description = "Specialist", UserId = 3 }
            };
        }

        public static IEnumerable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new Patient { Id = 1, FName = "Ali", LName = "Hassan", Age = 30, Gender = Gender.Male, Phone = "0100000001", UserId = 4 },
                new Patient { Id = 2, FName = "Sara", LName = "Youssef", Age = 25, Gender = Gender.Female, Phone = "0100000002", UserId = 5 },
                };
        }

        public static IEnumerable<Availability> GetAvailabilities()
        {
            return new List<Availability>
            {
                new Availability { Id = 1, DoctorId = 1, Day = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                new Availability { Id = 2, DoctorId = 1, Day = DayOfWeek.Wednesday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(13, 0, 0) },
                new Availability { Id = 3, DoctorId = 2, Day = DayOfWeek.Tuesday, StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(14, 0, 0) },
                new Availability { Id = 4, DoctorId = 2, Day = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                new Availability { Id = 5, DoctorId = 1, Day = DayOfWeek.Friday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0) }
            };
        }

        public static IEnumerable<Appointment> GetAppointments()
        {
            return new List<Appointment>
            {
                new Appointment { Id = 1, DoctorId = 1, PatientId = 1, AppointmentDate = new DateTime(2025, 7, 1, 10, 0, 0), Status = AppointmentStatus.Scheduled },
                new Appointment { Id = 2, DoctorId = 1, PatientId = 2, AppointmentDate = new DateTime(2025, 7, 2, 11, 0, 0), Status = AppointmentStatus.Completed },
                new Appointment { Id = 3, DoctorId = 2, PatientId = 1, AppointmentDate = new DateTime(2025, 7, 3, 12, 0, 0), Status = AppointmentStatus.Cancelled },
                new Appointment { Id = 4, DoctorId = 2, PatientId = 2, AppointmentDate = new DateTime(2025, 7, 4, 9, 0, 0), Status = AppointmentStatus.Scheduled },
                new Appointment { Id = 5, DoctorId = 1, PatientId = 1, AppointmentDate = new DateTime(2025, 7, 5, 13, 0, 0), Status = AppointmentStatus.NoShow }
            };
        }
    }
}
