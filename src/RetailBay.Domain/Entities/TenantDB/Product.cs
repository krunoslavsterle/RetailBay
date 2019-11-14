﻿using System;
using System.Collections.Generic;

namespace RetailBay.Domain.Entities.TenantDB
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public Guid ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ProductPrice ProductPrice { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}