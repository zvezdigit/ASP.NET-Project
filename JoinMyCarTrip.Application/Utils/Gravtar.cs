using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Utils
{
    public static class Gravtar
    {
        private const string GravatarApiBaseUrl = "https://www.gravatar.com/avatar/";
        public static string GetUrl(string email)
        {
            return $"{GravatarApiBaseUrl}{Hashing.MD5Hash(email)}";
        }
    }
}
