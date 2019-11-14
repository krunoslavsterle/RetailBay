using System.ComponentModel.DataAnnotations;

namespace RetailBay.Domain.Entities
{
    /// <summary>
    /// LookupEntityBase class.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Entities.EntityBase" />
    public abstract class LookupEntityBase : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Abrv { get; set; }

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
