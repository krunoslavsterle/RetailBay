﻿using Microsoft.Extensions.Logging;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Domain.Entities.TenantDB;

namespace RetailBay.Infrastructure.EntityFramework.Repositories
{
    /// <summary>
    /// ProductRepository implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Infrastructure.EntityFramework.Repository{RetailBay.Core.Entities.TenantDB.Product}" />
    /// <seealso cref="RetailBay.Core.Interfaces.Repositories.IProductRepository" />
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductRepository(TenantDBContext context, ILogger<ProductRepository> logger) : base(context, logger)
        {
        }
    }
}
