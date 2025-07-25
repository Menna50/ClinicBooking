🏥 Clinic Booking System
A comprehensive clinic appointment booking platform designed to seamlessly connect patients and doctors. The system provides a robust API for managing doctor profiles, schedules, and appointment bookings, with full support for distinct user roles: Patient, Doctor, and Admin.

✨ Key Features
🔐 Authentication & Authorization
Secure login/registration for both Patients and Doctors.

JWT-based authentication for secure, stateless sessions.

Granular role-based access control (Admin, Doctor, Patient).

👨‍⚕️ Doctor Management
Admin-Only Doctor Creation: Only Admins can add doctors.

Self-Service Profile Editing: Doctors can manage their profiles.

Public Profile Viewing: Doctor profiles are visible to all users.

🩺 Specialty Management
Admin Full Control: Admins can create, update, and delete medical specialties.

📅 Availability Scheduling
Doctor-Managed Schedules: Doctors can define their availability (days, times, slot durations).

Conflict Prevention: Overlap checks prevent scheduling conflicts.

🧑‍💼 Patient Management
Self-Registration & Profile Editing: Patients can register and manage their own profiles.

📆 Appointment Booking
Step-by-Step Booking Flow:

Choose a specialty

Select a doctor

Pick from available slots

Flexible Cancellations:

Patients: Cancel their own bookings

Doctors: Cancel their own appointments

Admins: Cancel any appointment

Status Management: Admins can update statuses (Confirmed, Completed, No-Show).

Personalized Views:

Patients: See all their appointments

Doctors: See all scheduled appointments

✅ Input Validation & Logging
FluentValidation: Applied globally for consistent, strong input validation.

Serilog: Logs all key actions and errors to console and files.

🌐 Clean RESTful API
Well-organized, scalable, and maintainable API structure.

🛠️ Technologies Used
Tech	Purpose
.NET Core (ASP.NET API)	Backend framework
Entity Framework Core	ORM for database interaction
SQL Server	Relational database
FluentValidation	Input validation
Serilog	Logging
AutoMapper	DTO and entity mapping
JWT	Authentication & authorization
Swagger/OpenAPI	API documentation & testing

