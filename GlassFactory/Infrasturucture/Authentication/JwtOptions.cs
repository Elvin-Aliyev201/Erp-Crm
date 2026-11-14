using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasturucture.Authentication
{
    public  sealed class JwtOptions
    {
        public const string SectionName = "Jwt";
        public string Issuer { get; set; }=default!;

        public string Audience { get; set; }

        public string SecretKey { get; set; } 

        public int ExpirationMinutes { get; set; }
    }
}
