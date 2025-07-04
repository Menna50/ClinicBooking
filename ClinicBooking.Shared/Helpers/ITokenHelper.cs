using ClinicBooking.Shared.Dtos;

namespace ClinicBooking.API.Helpers
{
    public interface ITokenHelper
    {
       (string token,DateTime expriesAt) GenerateToken(
          JwtUserModel jwtUserModel);
    }
}

