using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
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
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }
        [Authorize(Roles = "Doctor")]
        [HttpPost]
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
        [Authorize(Roles = "Doctor")]

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
        //    {
        //        return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
        //    }
        //    var res = await _availabilityService.GetAllByDoctorId(userId);
        //    if (!res.IsSuccess)
        //        return StatusCode(res.StatusCode, res.Error);
        //    return StatusCode(res.StatusCode,res.Data);
        //}
        [Authorize(Roles = "Doctor")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int availabilityId)
        {
            var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.GeById(availabilityId,userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode, res.Data);
        }
        [Authorize(Roles = "Doctor")]

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int availabilityId,UpdateAvailabilityRequestDto requestDto)
        {
            var currentUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (currentUserClaim == null || !int.TryParse(currentUserClaim.Value, out int userId))
            {
                return Unauthorized(new Error(AuthErrorCodes.InvalidToken, "User ID not found in token."));
            }
            var res = await _availabilityService.UpdateAvailabilityAsync(availabilityId,requestDto,userId);
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
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
        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(int doctorId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _availabilityService.GetAvailableSlotsByDoctorIdAsync(doctorId, fromDate, toDate);
            return StatusCode(result.StatusCode, result);
        }


    }

}
