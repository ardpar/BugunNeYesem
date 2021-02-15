using System;

namespace BugunNeYesem.Infrastructure.Abstract
{
    public interface ICryptoService
    { 
        string MD5Hash(string input);

        bool MD5HashCheck(string input, string hash);
 
    }
}