using Application.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrasturucture.Authentication
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; 

        private const int HashSize = 32;

        private const int Iterations = 210_000;


        private const string HashAlgorithm = "PBKDF2-SHA512";


        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA512, HashSize);

            return $"{HashAlgorithm}.{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }


        public bool VerifyHash(string password, string passwordHash)
        {
            var parts = passwordHash.Split('.');

            if(parts.Length != 4 || parts[0] != HashAlgorithm || !int.TryParse(parts[1], out int iterations))
            {
                return false;
            }

            byte[] salt;
            byte[] expected;

            try
            {
                salt= Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch(FormatException)
            {
                return false;
            }

            var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, expected.Length);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

       
    }
}
