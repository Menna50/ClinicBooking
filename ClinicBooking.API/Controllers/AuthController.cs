using ClinicBooking.Shared.Dtos;
using ClinicBooking.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicBooking.BLL.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using ClinicBooking.DAL.Data.Entities;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ClinicBooking.Shared.Results;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<RegisterRequestDto> _validator;
        private readonly IPatientService _patientService;
        
        public AuthController(IPatientService patientService,IAuthService authService,IValidator<RegisterRequestDto> validator)
        {
            _authService = authService;
            _validator = validator;
            _patientService = patientService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var res = await _authService.LoginAsync(request);

            if (res.IsSuccess)
                return StatusCode(res.StatusCode, res.Data);
            return StatusCode(res.StatusCode, res.Error);
        }
        //[HttpPost]
        //public async Task<IActionResult> addadmin(RegisterRequestDto request)
        //{

        //    _authService.RegisterAsync(request);
        //    return Ok();

        //}
        [HttpPost("register")]

        public async Task<IActionResult> Register(PatientRegisterRequestDto request)
        {

         
            var res =  await _patientService.AddAsync(request);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode, res.Data);
            return StatusCode(res.StatusCode, res.Error);
        }
       
    }
}
