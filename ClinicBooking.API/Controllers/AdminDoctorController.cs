using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.API.Controllers
{

    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public AdminDoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost("doctors")]
        public async Task<IActionResult> AddDoctor(DoctorRegisterDto dto)
        {
             var res=   await _doctorService.AddDoctorAsync(dto);
                if(res.IsSuccess)
                   return StatusCode(res.StatusCode,res.Data);
                return StatusCode(res.StatusCode, res.Error);
       

        }
        [HttpDelete("doctors/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var res = await _doctorService.DeleteDoctorAsync(id);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode);
            return StatusCode(res.StatusCode, res.Error);


        }
        [HttpGet("doctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] bool includeDeleted = false)
        {
            var result = await _doctorService.GetAllDoctorsAsync(includeDeleted);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Data : result.Error);
        }
        [HttpGet("doctors/{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return StatusCode(result.StatusCode, result.IsSuccess ? result.Data : result.Error);
        }

    }
}
