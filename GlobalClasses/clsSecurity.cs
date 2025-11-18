using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GMS.GlobalClasses
{
    public class clsSecurity
    {
        public static string ComputeHash(string Password) 
        {
            using (SHA256 sha256 = SHA256.Create()) 
            {
                byte[] HashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return BitConverter.ToString(HashedPassword).Replace("-", "").ToLower();
            }
        }
    }
}
