using Shopper_Demo.DTOs;
using Shopper_Demo.Entities;


namespace Shopper_Demo.Interfaces
{
    public interface IInMemoryShopper
    {
        void CreateShopper(Shopper shop);

        void DeleteShopper(Guid shopperid);

        void UpdateShopper(Shopper shop);

        IEnumerable<Shopper> GetShoppers();

        Shopper GetShopperById(Guid ShopperId);

        IEnumerable<Order> GetOrdersCompleted(Guid ShopperId);

        IEnumerable<Order> GetPastOrders(Guid ShopperId);

    }
}