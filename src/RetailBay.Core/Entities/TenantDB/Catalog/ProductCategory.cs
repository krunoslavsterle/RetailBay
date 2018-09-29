using System.Collections.Generic;

namespace RetailBay.Core.Entities.TenantDB
{
    public class ProductCategory : LookupEntityBase
    {
        public IList<Product> Products { get; set; }
    }
}
