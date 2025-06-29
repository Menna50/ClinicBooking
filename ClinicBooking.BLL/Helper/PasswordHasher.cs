using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.BLL.Helper
{
    public class PasswordHasher
    {
        public static (byte[] hashedPassword, byte[] salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // 128-bit salt

            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return (hashed, salt);
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        {
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return hashed.SequenceEqual(storedHash); 
        }

    }

}
