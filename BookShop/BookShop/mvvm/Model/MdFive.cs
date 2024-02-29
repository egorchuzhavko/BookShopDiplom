using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace BookShop.mvvm.Model
{
    public class MdFive
    {
        public static string GetHash(string password) {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
