using System;

namespace EventsAPI.Entities
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
