using System;
using Shopper_Demo.Entities;
// using Shopper_Demo.Interfaces;

namespace Shopper_Demo.Interfaces
{
    public interface IInMemoryRepository
    {
        OrderItem GetOrderItem(Guid Id);
        IEnumerable<OrderItem> GetOrderItems();
        void CreateOrderItem(OrderItem item);

        void UpdateOrderItem(OrderItem item);

        void DeleteOrderItem(Guid Id);


    }

}