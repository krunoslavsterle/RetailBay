using System;
using System.Collections.Generic;

namespace RetailBay.Application.Products.Queries.GetEditProductVM
{
    public class EditProductVM
    {
        public EditProductDTO Product { get; set; }
        public IDictionary<Guid, string> Categories { get; set; }
    }
}
