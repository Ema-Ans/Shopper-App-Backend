using Shopper_Demo.DTOs;
using Shopper_Demo.Entities;

namespace Shopper_Demo.Interfaces
{
    public interface IInMemoryOrder
    {
        Order? GetOrderById(Guid OrderId);
        Order? GetOrderbyShopperId(Guid ShopperId);
        IEnumerable<Order> GetOrders();
        void CreateOrder(Order ord);
        void  UpdateOrder(Order ord);
        void  DeleteOrder(Guid OrderId);
        void AssignOrder(Guid OrderId, Guid ShopperId);

        void CompleteOrder(Guid OrderId, Guid ShopperId);

    }
}