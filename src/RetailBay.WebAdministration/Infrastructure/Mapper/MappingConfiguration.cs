using AgileObjects.AgileMapper.Configuration;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.WebAdministration.Infrastructure.Mapper
{
    public class MappingConfiguration : MapperConfiguration
    {
        protected override void Configure()
        {
            WhenMapping
                .From<Product>()
                .To<Areas.Catalog.Models.ProductsViewModel.ProductDTO>()
                .Map(ctx => ctx.Source.ProductPrice.Price)
                .To(dto => dto.Price);

            WhenMapping
                .From<Areas.Catalog.Models.EditProductViewModel>()
                .Over<Product>()
                .Map(ctx => ctx.Source.Price)
                .To(domain => domain.ProductPrice.Price);

            // Cache all Product -> ProductDto mapping plans:
            GetPlansFor<Product>().To<Areas.Catalog.Models.ProductsViewModel.ProductDTO>();
            GetPlanFor<Areas.Catalog.Models.EditProductViewModel>().Over<Product>();
        }
    }
}
