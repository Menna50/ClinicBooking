using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicBooking.Shared.Dtos;

using ClinicBooking.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ClinicBooking.Shared.ErrorCodes;
using System.Security.Claims;
using ClinicBooking.Shared.Results;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfile()
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error("User ID not found in token.", AuthErrorCodes.InvalidToken));
            }
            var res = await _doctorService.GetDoctorProfileAsync(currentUserId);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode, res.Data);
            return StatusCode(res.StatusCode, res.Error);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorsBySpecialty(int specialtyId)
        {
         
            var res = await _doctorService.GetAllDoctorBySpecialtyIdAsync(specialtyId);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode, res.Data);
            return StatusCode(res.StatusCode, res.Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctorProfile(UpdateDoctorProfileDto dto)
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken,"User ID not found in token."));
            }
            var res = await _doctorService.UpdateDoctorProfile(dto, currentUserId);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode);
            return StatusCode(res.StatusCode, res.Error);
        }
    }
}
