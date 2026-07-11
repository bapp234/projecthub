using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset? UpdatedAt { get; protected set; }

        public Guid? CreatedBy { get; protected set; }

        public Guid? UpdatedBy { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTimeOffset? DeletedAt { get; protected set; }

        public Guid? DeletedBy { get; protected set; }
    }
}
