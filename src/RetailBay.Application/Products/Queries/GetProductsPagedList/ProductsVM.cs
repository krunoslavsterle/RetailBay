using RetailBay.Common.Collections;
using System;
using System.Collections.Generic;

namespace RetailBay.Application.Products.Queries.GetProductsPagedList
{
    public class ProductsVM
    {
        public IPagedCollection<ProductDTO> Products { get; set; }
        public IDictionary<Guid, string> Categories { get; set; }
    }
}
