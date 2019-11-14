using System.Collections.Generic;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class ProductCategory : LookupEntityBase
    {
        public IList<Product> Products { get; set; }
    }
}
