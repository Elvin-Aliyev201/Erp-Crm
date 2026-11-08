using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool VerifyHash(string password, string passwordHash);
    }
}
