using System;
using System.Linq;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {

            //SeedIdentityDB();
            SeedProductCategoryTable();
            //SeedingProductTable();
            Console.Read();
        }

        private static void SeedProductCategoryTable()
        {
            Console.WriteLine("Seeding ProductCategory table...");

            var factory = new TenantContextFactory();
            var context = factory.CreateDbContext(null);

            context.ProductCategories.Add(new ProductCategory
            {
                Id = Guid.NewGuid(),
                Abrv = "ELC",
                Name = "Electronics",
                Slug = "electronics",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsDeleted = false
            });

            context.ProductCategories.Add(new ProductCategory
            {
                Id = Guid.NewGuid(),
                Abrv = "FDD",
                Name = "Food",
                Slug = "food",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsDeleted = false
            });

            try
            {
                context.SaveChanges();

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: /n{ex.Message}");
            }
        }

        private static void SeedingProductTable()
        {
            Console.WriteLine("Seeding Product table...");

            var factory = new TenantContextFactory();
            var context = factory.CreateDbContext(null);

            var category = context.ProductCategories.First();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "RetailBay Test Product",
                Description = "Some long description",
                ProductCategoryId = category.Id,
                IsPublished = false,
                Slug = "retail_bay_test_product",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            var productPrice = new ProductPrice
            {
                Id = product.Id,
                Price = 22.99M,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            product.ProductPrice = productPrice;
            context.Products.Add(product);

            try
            {
                context.SaveChanges();

                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: /n{ex.Message}");
            }
        }

        private static void SeedIdentityDB()
        {
            var factory = new IdentityDBContextFactory();
            var ctx = factory.CreateDbContext(null);

            var role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            };

            ctx.Roles.Add(role);
            ctx.SaveChanges();
        }
    }
}
