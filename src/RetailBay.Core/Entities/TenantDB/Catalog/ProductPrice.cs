namespace RetailBay.Core.Entities.TenantDB
{
    public class ProductPrice : EntityBase
    {
        public decimal Price { get; set; }

        public Product Product { get; set; }
    }
}
