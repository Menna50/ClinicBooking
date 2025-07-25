using ClinicBooking.BLL.Services.Interfaces;
using ClinicBooking.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
            
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task< IActionResult> GetAll()
        {
            var res =await _specialtyService.GetAllAsync();
            if (!res.IsSuccess)
                return StatusCode(res.StatusCode, res.Error);
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _specialtyService.GetByIdAsync(id);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode, res.Data);
            return StatusCode(res.StatusCode, res.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpecialtyDto dto)
        {
            var res = await _specialtyService.AddAsync(dto);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode);
            return StatusCode(res.StatusCode, res.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateSpecialtyDto dto)
        {
            var res = await _specialtyService.UpdateAsync(dto);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode);
            return StatusCode(res.StatusCode, res.Error);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _specialtyService.DeleteAsync(id);
            if (res.IsSuccess)
                return StatusCode(res.StatusCode);
            return StatusCode(res.StatusCode, res.Error);
        }
    }
}
