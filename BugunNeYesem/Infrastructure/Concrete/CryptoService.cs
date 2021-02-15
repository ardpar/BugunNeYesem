using BugunNeYesem.Infrastructure.Abstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text; 

namespace BugunNeYesem.Infrastructure.Concrete
{
    public class CryptoService : ICryptoService
    { 
        public string MD5Hash(string input)
        {
            using var md5 = MD5.Create();
            return GetMd5Hash(md5, input);
        }

        public bool MD5HashCheck(string input, string hash)
        {
            using var md5 = MD5.Create();
            return VerifyMd5Hash(md5, input, hash);
        }
  

        private string GetMd5Hash(HashAlgorithm md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private bool VerifyMd5Hash(HashAlgorithm md5Hash, string input, string hash)
        {
            var hashOfInput = GetMd5Hash(md5Hash, input);

            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

      
    }
}