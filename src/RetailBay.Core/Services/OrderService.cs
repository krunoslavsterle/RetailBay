﻿using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;
using RetailBay.Domain.Entities.TenantDB;
using RetailBay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// OrderService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.IOrderService" />
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingAddressService _shippingAddressService;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService" /> class.
        /// </summary>
        /// <param name="orderRepository">The Order repository.</param>
        /// <param name="cartItemRepository">The CartItem repository.</param>
        /// <param name="cartRepository">The Cart repository.</param>
        /// <param name="shippingAddressService">The shipping address service.</param>
        public OrderService(IOrderRepository orderRepository, ICartItemRepository cartItemRepository, ICartRepository cartRepository, IShippingAddressService shippingAddressService)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _shippingAddressService = shippingAddressService;
        }
        
        /// <summary>
        /// Gets list of all <see cref="Order"/> asynchronous.
        /// </summary>
        /// <returns>List of all <see cref="Order"/> asynchronous.</returns>
        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets the <see cref="Order" /> by id asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>The <see cref="Order" /></returns>
        public Task<Order> GetOrderAsync(Guid id, params string[] includeProperties)
        {
            return _orderRepository.GetOneAsync(p => p.Id == id, includeProperties);
        }

        /// <summary>
        /// Gets the orders for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId)
        {
            return _orderRepository.GetAsync(p => p.UserId == userId);
        }
        
        /// <summary>
        /// Creates the order for user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        public async Task CreateOrderForUserAsync(Guid? userId, Guid cartId)
        {
            var addressTask = _shippingAddressService.GetShippingAddressForCartAsync(cartId);
            var cartItemsTask = _cartItemRepository.GetAsync(p => p.CartId == cartId, null, $"{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            await Task.WhenAll(addressTask, cartItemsTask);

            var address = await addressTask;
            var cartItems = await cartItemsTask;

            if (cartItems == null || cartItems.Count() == 0)
                throw new Exception("No cart items found");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ShippingAddressId = address.Id,
                OrderStatus = OrderStatus.New
            };

            var orderItems = new List<OrderItem>();
            var products = cartItems.Select(p => p.Product);
            foreach (var product in products)
            {
                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductPrice = product.ProductPrice.Price,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });
            }

            order.OrderItems = orderItems;
            order.OrderTotal = orderItems.Sum(p => p.ProductPrice);

            await _orderRepository.InsertAsync(order, false);
            await _cartRepository.DeleteAsync(cartId, false);
            await _cartRepository.SaveAsync();
        }
    }
}
