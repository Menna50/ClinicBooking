using ClinicBooking.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Shared.Dtos
{
    public class AuthResponseDto
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;    
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }              
    }
}
