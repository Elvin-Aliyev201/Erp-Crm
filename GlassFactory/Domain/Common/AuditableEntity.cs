using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AuditableEntity:BaseEntity
    {
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? UpdatedOnUtc { get; private set; }
        public string CreatedBy { get; private set; }

        public string UpdatedBy { get; private set; }


    }
}
