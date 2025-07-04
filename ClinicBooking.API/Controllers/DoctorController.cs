using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicBooking.Shared.Dtos;

using ClinicBooking.BLL.Services.Interfaces;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task< IActionResult >AddDoctor(DoctorRegisterDto dto)
        {
            try
            {
             await   _service.AddDoctorAsync(dto);
                return Created();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
             
        }
    }
}
