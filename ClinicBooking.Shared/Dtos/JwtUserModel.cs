using ClinicBooking.DAL.Data.Enums;

namespace ClinicBooking.Shared.Dtos
{
    public class JwtUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
