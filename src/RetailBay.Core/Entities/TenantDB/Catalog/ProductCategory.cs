using System.Collections.Generic;

namespace RetailBay.Core.Entities.TenantDB
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }

        public IList<Product> Products { get; set; }
    }
}
