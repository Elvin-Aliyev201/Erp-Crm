using Application.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasturucture.Time
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
