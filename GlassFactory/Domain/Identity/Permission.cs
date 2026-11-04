using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity
{
    public sealed class Permission:BaseEntity
    {
        public string Code { get; private set; }
        public string Description { get; private set; }
        private Permission(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static Permission Create(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Icaze kod bos ola bilmez");

            code = code.Trim().ToUpperInvariant();
            description = description.Trim()??string.Empty;

            return new Permission(code, description);
        }
    }
}
