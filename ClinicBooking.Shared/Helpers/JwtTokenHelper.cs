using ClinicBooking.API.Configurations;
using ClinicBooking.Shared.Dtos;
using ClinicBooking.API.Configurations;
using ClinicBooking.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace ClinicBooking.API.Helpers
{
    public class JwtTokenHelper : ITokenHelper
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenHelper(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(JwtUserModel jwtUserModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,jwtUserModel.Id.ToString()),
                 new Claim(ClaimTypes.Name,jwtUserModel.Username),
                  new Claim(ClaimTypes.Role,jwtUserModel.Role.ToString()),

            };
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.LifeTime),
                signingCredentials: new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256),
                claims:claims

            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}
