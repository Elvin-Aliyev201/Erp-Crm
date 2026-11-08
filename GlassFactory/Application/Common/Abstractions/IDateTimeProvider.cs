using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
