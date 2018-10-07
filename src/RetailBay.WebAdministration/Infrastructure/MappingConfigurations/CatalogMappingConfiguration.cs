using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileObjects.AgileMapper.Configuration;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.WebAdministration.Infrastructure.MappingConfigurations
{
    public class CatalogMappingConfiguration : MapperConfiguration
    {
        protected override void Configure()
        {
            WhenMapping
                .From<Product>()
                .ToANew<Areas.Catalog.Models.ProductsViewModel.ProductDTO>()
                .Map((m, c) => m.ProductPrice.Price)
                .To(c => c.Price);

            // Cache all Product -> ProductDto mapping plans:
            GetPlansFor<Product>().To<Areas.Catalog.Models.ProductsViewModel.ProductDTO>();
        }
    }
}
