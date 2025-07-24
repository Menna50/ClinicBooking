using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IAppointmentService
    {
        public  Task<ResultT<string>> BookAppointmentAsync(CreateAppointmentRequestDto dto,int userId);
        Task<ResultT<List<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int userId, bool upcomingOnly);
        Task<ResultT<List<AppointmentDto>>> GetAppointmentsByPatientIdAsync(int userId, bool upcomingOnly);
        public Task<Result> CancelAppointmentAsync(int appointmentId, int currentUserId, Roles currentUserRole);
        public Task<Result> UpdateAppointmentStatusAsync(int appointmentId, AppointmentStatus status);
    }
}
