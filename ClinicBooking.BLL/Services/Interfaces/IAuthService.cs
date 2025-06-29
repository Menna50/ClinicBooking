using ClinicBooking.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> RegisterAsync(UserRegisterDto dto);
        public  Task<UserDto> LoginAsync(UserLoginDto dto);
     

    }
}
