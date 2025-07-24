using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public AdminDoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorRegisterDto dto)
        {
             var res=   await _doctorService.AddDoctorAsync(dto);
                if(res.IsSuccess)
                   return StatusCode(res.StatusCode,res.Data);
                return StatusCode(res.StatusCode, res.Error);
       

        }

    }
}
