using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailBay.Core.Entities.TenantDB;
using RetailBay.Core.Interfaces;
using RetailBay.Core.Interfaces.Repositories;

namespace RetailBay.Core.Services
{
    /// <summary>
    /// OrderService implementation.
    /// </summary>
    /// <seealso cref="RetailBay.Core.Interfaces.IOrderService" />
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;

        #endregion Fields

        #region Constructors

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ICartItemRepository cartItemRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
        }

        #endregion Constructors

        #region Properties



        #endregion Properties

        #region Methods
        
        /// <summary>
        /// Gets list of all <see cref="Order"/> asynchronous.
        /// </summary>
        /// <returns>List of all <see cref="Order"/> asynchronous.</returns>
        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return _orderRepository.GetAllAsync();
        }
        
        /// <summary>
        /// Creates the order for user asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="shippingAddressId">The shipping address identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No cart items found</exception>
        public async Task CreateOrderForUserAsync(Guid userId, Guid cartId, Guid shippingAddressId)
        {
            var cartItems = await _cartItemRepository.GetAsync(p => p.CartId == cartId, null, $"{nameof(CartItem.Product)}.{nameof(Product.ProductPrice)}");
            if (cartItems == null || cartItems.Count() == 0)
                throw new Exception("No cart items found");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ShippingAddressId = shippingAddressId,
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

        #endregion Methods
    }
}
