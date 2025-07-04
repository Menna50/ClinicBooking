using ClinicBooking.Shared.Dtos;
using ClinicBooking.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ResultT<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto);
        public  Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
     

    }
}
