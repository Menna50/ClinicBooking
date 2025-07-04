using ClinicBooking.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
         public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }

        public Role Role { get; set; }

        public Patient Patient { get; set; }  // Optional
        public Doctor Doctor { get; set; }// Optional
    }
 

}
