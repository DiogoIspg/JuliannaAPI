using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore.Entity
{
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; }
    }
}