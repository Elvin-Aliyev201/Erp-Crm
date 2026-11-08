using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        string UserName { get; }

        bool IsAuthenticated { get; }
    }
}
