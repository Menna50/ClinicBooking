using ClinicBooking.BLL.Services.Implementations;
using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.DAL.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            return  Ok(patients);
        }

    }
}
