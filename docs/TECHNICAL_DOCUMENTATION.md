 System Overview
The Clinic Appointment Booking System is a robust software solution engineered to streamline appointment scheduling between patients and medical professionals.

Provides a RESTful API supporting multiple roles (Patient, Doctor, Admin).

Prioritizes security, scalability, and user experience.

1.1 Core Features
🔐 User and Role Management
Secure authentication and authorization using JWT.

Role-based access control for Patients, Doctors, Administrators.

👤 Patient & Doctor Profile Management
Create, update, and view personal profiles.

🩺 Medical Specialty Management
Full CRUD (Create, Read, Update, Delete) for managing specialties.

📅 Doctor Availability Management
Doctors can define and manage working schedules.

Includes overlap detection to avoid conflicts.

📆 Advanced Appointment Booking
Dynamic Slot Search → search available slots by specialty, doctor, date, time.

Flexible Booking → direct booking of available slots.

Multi-Role Cancellation → cancellation allowed for Patients, Doctors, Admins.

Admin-Exclusive Status Updates → Admin can change appointment status (Scheduled → Completed, Cancelled, NoShow).

⚠️ Comprehensive Error Handling
Custom Exception Handling Middleware → standardizes API error responses.

Result Pattern → unified success/failure result wrapper.

Structured Error Handling → predefined error codes + descriptive messages.

Input Validation → FluentValidation with global filters.

Centralized Logging → Serilog logs to console and files.

2️⃣ Core System Data
This section defines system-level data: default admin credentials and key enumerations.

2.1 Initial Administrator Credentials
Used for first-time access:

makefile
 
 
Username: admin  
Password: 12345678
⚠️ Important: Change immediately for production with a strong password.

2.2 Enumeration Definitions
👥 Roles
 
 
 
namespace ClinicBooking.DAL.Data.Enums
{
    public enum Roles
    {
        None = 0,    
        Admin = 1,   
        Doctor = 2,  
        Patient = 3  
    }
}
📌 Appointment Status
 
 
 
namespace ClinicBooking.DAL.Data.Enums
{
    public enum AppointmentStatus
    {
        Scheduled = 0,
        Completed = 1,
        Cancelled = 2,
        NoShow = 3
    }
}
🚻 Gender
 
 
 
namespace ClinicBooking.DAL.Data.Enums
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}
3️⃣ Key User Flows
Details interaction sequences for different user roles.

3.1 Patient Appointment Booking Flow
Authentication:

POST /api/Auth/Register → Self-registration

POST /api/Auth/Login → JWT token

Browse Specialties: GET /api/Specialty/GetAll

Select Specialty & View Doctors:

GET /api/Doctor/GetDoctorsBySpecialty?specialtyId={id}

Select Doctor & View Slots:

GET /api/Appointment/GetAvailableSlots?doctorId={id}&date={date}

Book Appointment:

POST /api/Appointment/BookAppointment

Manage Appointments:

View → GET /api/Appointment/GetPatientAppointments

Cancel → PUT /api/Appointment/CancelAppointment

3.2 Doctor Management Flow
Authentication: POST /api/Auth/Login

Profile:

View → GET /api/Doctor/GetDoctorProfile

Update → PUT /api/Doctor/UpdateDoctorProfile

Availability:

Add → POST /api/Availability/Add

View → GET /api/Availability/GetAll

Update → PUT /api/Availability/Update

Delete → DELETE /api/Availability/Delete

Appointments:

View → GET /api/Appointment/GetDoctorAppointments

Cancel → PUT /api/Appointment/CancelAppointment

3.3 Administrator Management Flow
Authentication: POST /api/Auth/Login

User Management:

Get All Doctors → GET /api/AdminDoctor/GetAllDoctors

Add Doctor → POST /api/AdminDoctor/AddDoctor

Delete Doctor → DELETE /api/AdminDoctor/DeleteDoctor

Specialties:

Create → POST /api/Specialty/Create

Update → PUT /api/Specialty/Update

Delete → DELETE /api/Specialty/Delete

Appointments:

Update Status → PUT /api/Appointment/UpdateAppointmentStatus

(Future) View All, Manual Book, Reschedule


4️⃣ Project Architecture
The system follows clean, layered architecture for maintainability and scalability.

ClinicBooking.API: Entry point, controllers, middleware, startup configs.

ClinicBooking.Services: Business logic, service classes, Result pattern.

ClinicBooking.DAL: Database interactions, EF Core, repositories, entities, enums.

ClinicBooking.Shared: DTOs, validators, result classes, error codes.

ClinicBooking.BLL: AutoMapper profiles, helpers (PasswordHasher, JwtHelper).

✅ Best Practices
Custom Exception Handling Middleware

Result Pattern for operation outcomes

Structured Error Handling with codes

Global FluentValidation

Serilog for logging

5️⃣Technologies Used
Backend: .NET Core (ASP.NET Core Web API)

Database: SQL Server

ORM: Entity Framework Core

Validation: FluentValidation

Logging: Serilog

Mapping: AutoMapper

Auth: JWT

API Docs: Swagger/OpenAPI

6️⃣ Future Enhancements
Appointment Rescheduling

Doctor Reviews and Ratings

Notifications (Email/SMS)

Admin Dashboard

File Management (uploads, medical records)

Payment Gateway Integration

Dynamic Role Management via DB

7️⃣ Getting Started
Refer to README.md for:

Prerequisites

Installation

Running instructions

