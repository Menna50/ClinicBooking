using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.ErrorCodes;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("me")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GeProfile()
        {

            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _patientService.GetProfileAsync(currentUserId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpPut("me")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Update(UpdatePatientProfileDto dto)
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _patientService.UpdateAsync(dto, currentUserId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode);

        }


    }
}
