using ClinicBooking.DAL.Data.Entities;
using ClinicBooking.DAL.MennaRepo;
using ClinicBooking.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static ClinicBooking.API.Program;

namespace ClinicBooking.API.Controllers
{
    public class MyAppSettings
    {
        public string SiteTitle { get; set; }
        public int PageSize { get; set; }
    }
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepo<User> _mennaRepo;

        private readonly MyAppSettings _settings;

        
        
        public UserController(IGenericRepository<User> userRepo,
            IGenericRepo<User> mennaRepo,IOptions<MyAppSettings> options)
        {
            _settings = options.Value;
            
            _userRepo = userRepo;
            _mennaRepo = mennaRepo;

        }
        [HttpGet]
        public async Task<object> GetAll()
        {
            var users = await _mennaRepo.GetAllAsync();
            int d;
            return users;
        }
        [HttpGet]
        public string GetSettings()
        {
          
            return _settings.SiteTitle;
        }
    }
}
