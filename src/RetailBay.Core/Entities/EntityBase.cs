using System;
using System.ComponentModel.DataAnnotations;

namespace RetailBay.Core.Entities
{
    /// <summary>
    /// Entity base class.
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}
