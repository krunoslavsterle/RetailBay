using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.SystemDb
{
    [Table("tenant")]
    public class Tenant : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string HostName { get; set; }

        [Required]
        public string ConnectionString { get; set; }
    }
}
