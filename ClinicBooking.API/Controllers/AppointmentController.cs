using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Enums;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.ErrorCodes;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentRequestDto dto)
        {

            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var result = await _appointmentService.BookAppointmentAsync(dto, currentUserId);
        
         if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);
            return StatusCode(result.StatusCode, result.Error);
    }

        [HttpGet("doctor")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorAppointments([FromQuery] bool upcomingOnly = true)
        {

            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var result = await _appointmentService.GetAppointmentsByDoctorIdAsync(currentUserId, upcomingOnly);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);
            return StatusCode(result.StatusCode, result.Error);
        }

        [HttpGet("patient")] 
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetPatientAppointments( [FromQuery] bool upcomingOnly = true)
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var result = await _appointmentService.GetAppointmentsByPatientIdAsync(currentUserId, upcomingOnly);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);
            return StatusCode(result.StatusCode, result.Error);
        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin,Doctor,Patient")]

        public async Task<IActionResult> CancelAppointment(int id)
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var currentUserRoleClaim = User.FindFirst(ClaimTypes.Role);
            if (currentUserRoleClaim == null || !Roles.TryParse(currentUserIdClaim.Value, out Roles currentUserRole))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var result = await _appointmentService.CancelAppointmentAsync(id,currentUserId,currentUserRole);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode);
            return StatusCode(result.StatusCode, result.Error);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, [FromQuery] AppointmentStatus status)
        {
            var result = await _appointmentService.UpdateAppointmentStatusAsync(id, status);
            if (result.IsSuccess)
                return StatusCode(result.StatusCode);
            return StatusCode(result.StatusCode, result.Error);
        }
    }
}
