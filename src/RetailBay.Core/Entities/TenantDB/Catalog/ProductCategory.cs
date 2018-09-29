using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailBay.Core.Entities.TenantDB
{
    [Table("product_category")]
    public class ProductCategory : LookupEntityBase
    {
        public IList<Product> Products { get; set; }
    }
}
