using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Helpers
{
    public static class PasswordCheck
    {
        public static bool CheckPassword(this string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            bool areEqual = true;

            Parallel.For(0, computedHash.Length, (i, state) =>
            {
                if (passwordHash[i] != computedHash[i])
                {
                    areEqual = false;
                    state.Stop();
                }
            });

            return areEqual;
        }
    }
}
