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
                new User { Id = 1, UserName = "admin",Email= "admin@gmail.com",PhoneNumber="123456789", PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Roles.Admin },
                new User { Id = 2, UserName = "doctor1",Email= "doctor1@gmail.com", PhoneNumber="123456789",PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Roles.Doctor },
                new User { Id = 3, UserName = "doctor2",Email= "doctor2@gmail.com", PhoneNumber="123456789",PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Roles.Doctor },
                new User { Id = 4, UserName = "patient1",Email= "patient1@gmail.com", PhoneNumber="123456789",PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Roles.Patient },
                new User { Id = 5, UserName = "patient2",Email= "patient2@gmail.com", PhoneNumber="123456789",PasswordHash = new byte[] {}, PasswordSalt = new byte[] {}, Role = Roles.Patient }
            };
        }

        public static IEnumerable<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor { Id = 1, Name="doctor1", SpecialtyId = 1, Bio = "Senior Doctor",ConsultationFee=1000, UserId = 2 },
                new Doctor { Id = 2, Name="doctor2",SpecialtyId = 2, Bio = "Specialist", ConsultationFee=1000,UserId = 3 }
            };
        }

        public static IEnumerable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new Patient { Id = 1, FName = "Ali", LName = "Hassan", Age = 30, Gender = Gender.Male,UserId = 4 },
                new Patient { Id = 2, FName = "Sara", LName = "Youssef", Age = 25, Gender = Gender.Female,UserId = 5 },
                };
        }

        public static IEnumerable<Availability> GetAvailabilities()
        {
            return new List<Availability>
            {
                new Availability { Id = 1, DoctorId = 1, Day = DayOfWeek.Monday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(12, 0, 0) },
                new Availability { Id = 2, DoctorId = 1, Day = DayOfWeek.Wednesday, StartTime = new TimeOnly(10, 0, 0), EndTime = new TimeOnly(13, 0, 0) },
                new Availability { Id = 3, DoctorId = 2, Day = DayOfWeek.Tuesday, StartTime = new TimeOnly(11, 0, 0), EndTime = new TimeOnly(14, 0, 0) },
                new Availability { Id = 4, DoctorId = 2, Day = DayOfWeek.Thursday, StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(12, 0, 0) },
                new Availability { Id = 5, DoctorId = 1, Day = DayOfWeek.Friday, StartTime = new TimeOnly(10, 0, 0), EndTime = new TimeOnly(12, 30, 0) }
            };
        }
        public static List<Specialty> GetSpecialties()
        {
            return new List<Specialty>
            {
            new Specialty { Id = 1, Name = "Cardiology" },
            new Specialty { Id = 2, Name = "Dermatology" },
            new Specialty { Id = 3, Name = "Pediatrics" },
            new Specialty { Id = 4, Name = "Neurology" },
            new Specialty { Id = 5, Name = "Orthopedics" }
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
