using ClinicBooking.Shared.Dtos;

namespace ClinicBooking.API.Helpers
{
    public interface ITokenHelper
    {
       string GenerateToken(
          JwtUserModel jwtUserModel);
    }
}

