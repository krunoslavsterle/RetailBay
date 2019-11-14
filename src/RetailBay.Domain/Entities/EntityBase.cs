using System;

namespace RetailBay.Domain.Entities
{
    /// <summary>
    /// Entity base class.
    /// </summary>
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
