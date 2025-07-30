using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.ErrorCodes;
using ClinicBooking.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ClinicBooking.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }
       
        [HttpPost("doctor/")]
        public async Task<IActionResult> Add(AddAvailabilityRequestDto addAvailabilityRequestDto)
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null || !int.TryParse(userClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.Add(addAvailabilityRequestDto, userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode);

        }
 

        [HttpGet("doctor/getAvailability")]
        public async Task<IActionResult> GetById(int availabilityId)
        {
            var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.GeById(availabilityId, userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode, res.Data);
        }

        [HttpPut("doctor/")]
        public async Task<IActionResult> Update(int availabilityId, UpdateAvailabilityRequestDto requestDto)
        {
            var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.UpdateAvailabilityAsync(availabilityId, requestDto, userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode);
        }
        [HttpDelete("doctor/")]
       
        public async Task<IActionResult> Delete(int availabilityId)
        {
            var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.DeleteAsync(availabilityId, userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode);
        }
      
        [HttpGet("doctor/getAvailabilites")]
        public async Task<IActionResult> getDoctorAvailabilites()
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim.Value, out int currentUserId))
            {
                return Unauthorized(new Error("User ID not found in token.", AuthErrorCodes.InvalidToken));
            }
            var result = await _availabilityService.GetAllByDoctorId(currentUserId);
            return StatusCode(result.StatusCode, result.Data);
        }


        [AllowAnonymous]
        [HttpGet("getAvailableSlots")]
        public async Task<IActionResult> GetAvailableSlots
         (
            [FromQuery][Required] int doctorId,
            [FromQuery][Required] DateTime fromDate,
            [FromQuery][Required] DateTime toDate
         )
        {
            var result = await _availabilityService.GetAvailableSlotsByDoctorIdAsync(doctorId, fromDate, toDate);
            return StatusCode(result.StatusCode, result.Data);
        }


    }

}
