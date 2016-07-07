using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace com.paralib.Utils
{
    public class Crypto
    {
        public static string HashPassword(string password)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32, 1000);
            rfc2898DeriveBytes.IterationCount = 10000;
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;
            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }

        public static string HashPassword(string password, string salt)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000);
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            return salt + "|" + Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string hash, string password)
        {
            string[] parts = hash.Split('|');
            byte[] salt = null;
            byte[] hashedPassword = null;

            if (parts.Length == 2)
            {
                salt = Convert.FromBase64String(parts[0]);
                hashedPassword = Convert.FromBase64String(parts[1]);

                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);

                byte[] newHashedPassword = rfc2898DeriveBytes.GetBytes(20);

                return Convert.ToBase64String(hashedPassword) == Convert.ToBase64String(newHashedPassword);

            }
            else
            {
                throw new ParalibException("Unrecognized password format");
            }
        }


    }
}
