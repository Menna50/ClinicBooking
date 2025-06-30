using ClinicBooking.Shared.Dtos;
using ClinicBooking.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicBooking.BLL.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using ClinicBooking.DAL.Data.Entities;
using System.Text;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IAuthService _authService;
        public AuthController(ITokenHelper tokenHelper, IAuthService authService)
        {
            _tokenHelper = tokenHelper;
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var user = await _authService.LoginAsync(request);
            if (user == null)
                return BadRequest("Login Data is invalid");
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            int x = 5;
            var res = _authService.RegisterAsync(request);
            return Ok(res);
        }
    }
}
