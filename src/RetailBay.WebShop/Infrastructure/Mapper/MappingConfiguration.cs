using AgileObjects.AgileMapper.Configuration;
using RetailBay.Domain.Entities.TenantDB;
using RetailBay.WebShop.Models.Cart;

namespace RetailBay.WebShop.Infrastructure.Mapper
{
    public class MappingConfiguration : MapperConfiguration
    {
        protected override void Configure()
        {
            WhenMapping
                .From<CartItem>()
                .To<CartItemDTO>()
                .Map((ent, dto) => ent.Product.Name)
                .To(dto => dto.Name)
                .And
                .Map((ent, dto) => ent.Product.ProductPrice.Price)
                .To(dto => dto.Price)
                .And
                .Map((ent, dto) => ent.Id)
                .To(dto => dto.Id);

            GetPlansFor<CartItemDTO>().To<CartItem>();
        }
    }
}
