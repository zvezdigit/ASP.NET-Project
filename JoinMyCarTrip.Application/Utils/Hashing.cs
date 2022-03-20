using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Utils
{
    public static class Hashing
    {
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Convert.ToHexString(hashBytes).ToLowerInvariant();
            }
        }
    }
}
