using ClinicBooking.DAL.Data.Enums;

namespace ClinicBooking.Shared.Dtos
{

    public class UserRegisterDto
    {
       public string UserName {  get; set; }    
       public string Password { get; set; }    
        public Role Role { get; set; }
    }
}
