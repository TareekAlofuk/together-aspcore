using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOne.Security.Cryptography;
using DevOne.Security.Cryptography.BCrypt;
namespace together_aspcore.Shared
{
    public class BcryptHash
    {
        public static string Bcrypt(string password)
        {
            string salt = BCryptHelper.GenerateSalt();
            string hash = BCryptHelper.HashPassword(password, salt);
            return hash;
        }
    }
}
