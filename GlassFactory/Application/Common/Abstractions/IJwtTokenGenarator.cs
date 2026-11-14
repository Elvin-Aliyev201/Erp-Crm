using Domain.Identity;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions
{
    public interface IJwtTokenGenarator
    {
        AccessToken GenerateToken(User user);
    }

    public record AccessToken(string Token, DateTime Expiration);
}
